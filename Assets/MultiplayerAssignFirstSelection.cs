using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MultiplayerAssignFirstSelection : MonoBehaviour
{
    private MultiplayerEventSystem _mpSystem;
    [SerializeField] private string gameObjectNameToSelectFirst;
    
    // Start is called before the first frame update
    void Awake()
    {
        _mpSystem = GetComponent<MultiplayerEventSystem>();
    }

}
