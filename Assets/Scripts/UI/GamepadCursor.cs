using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;


namespace UI
{
    public class GamepadCursor : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private RectTransform cursorTransform;
        [SerializeField] private Canvas canvas;
        [SerializeField] private RectTransform canvasTransform;
        [SerializeField] private float cursorSpeed = 1000f;
        [SerializeField] private float screenPadding = 32f;
        private bool _previousMouseState;
        private Mouse _vMouse;
        private Mouse _currentMouse;
        private Camera _mainCamera;

        private string _previousControlScheme = "";
        private const string GamepadScheme = "Gamepad";
        private const string KeyboardAndMouseScheme = "Keyboard&Mouse";
        private void OnEnable()
        {
            _mainCamera = Camera.main;
            _currentMouse = Mouse.current;
            
            if (_vMouse == null) _vMouse = (Mouse)InputSystem.AddDevice("VirtualMouse");
            else if (!_vMouse.added)
                InputSystem.AddDevice(_vMouse);

            InputUser.PerformPairingWithDevice(_vMouse, playerInput.user);

            if (cursorTransform != null)
            {
                Vector2 position = cursorTransform.anchoredPosition;
                InputState.Change(_vMouse.position, position);
            }

            InputSystem.onAfterUpdate += UpdateMotion;
            playerInput.onControlsChanged += OnControlsChanged;
        }

        private void OnDisable()
        {
            
            if(_vMouse != null && _vMouse.added) InputSystem.RemoveDevice(_vMouse);
            InputSystem.onAfterUpdate -= UpdateMotion;
            playerInput.onControlsChanged -= OnControlsChanged;

        }

        void UpdateMotion()
        {
            if (_vMouse == null || Gamepad.current == null)
                return;
            
            Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
            deltaValue *= cursorSpeed * Time.deltaTime;

            Vector2 currentPosition = _vMouse.position.ReadValue();
            Vector2 newPosition = currentPosition + deltaValue;

            newPosition.x = Mathf.Clamp(newPosition.x, screenPadding, Screen.width - screenPadding);
            newPosition.y = Mathf.Clamp(newPosition.y, screenPadding, Screen.height - screenPadding);
            
            InputState.Change(_vMouse.position, newPosition);
            InputState.Change(_vMouse.delta, deltaValue);

            bool aButtonIsPressed = Gamepad.current.aButton.IsPressed();
            if (_previousMouseState != aButtonIsPressed)
            {
                _vMouse.CopyState<MouseState>(out var mouseState);
                mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
                InputState.Change(_vMouse, mouseState);
                _previousMouseState = aButtonIsPressed;
            }
            AnchorCursor(newPosition);
        }

        void AnchorCursor(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, position,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _mainCamera, out var anchoredPosition);
            cursorTransform.anchoredPosition = anchoredPosition;
        }
        
        private void OnControlsChanged(PlayerInput input)
        {
            if (playerInput.currentControlScheme == KeyboardAndMouseScheme && _previousControlScheme != KeyboardAndMouseScheme)
            {
                // This hides the gamepad cursor
                cursorTransform.gameObject.SetActive(false);
                // This will set the MOUSE visible
                Cursor.visible = true;
                _currentMouse.WarpCursorPosition(_vMouse.position.ReadValue());
                _previousControlScheme = KeyboardAndMouseScheme;
            } else if (playerInput.currentControlScheme == GamepadScheme && _previousControlScheme != GamepadScheme)
            {
                cursorTransform.gameObject.SetActive(true);
                Cursor.visible = false;
                InputState.Change(_vMouse.position,_currentMouse.position.ReadValue());
                AnchorCursor(_currentMouse.position.ReadValue());
                _previousControlScheme = GamepadScheme;
            }
        }
    }
}