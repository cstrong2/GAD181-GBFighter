using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUIv2 : MonoBehaviour
    {
        [SerializeField] public int charID;
        [SerializeField] public Image charImage;
        
        private void Awake()
        {
            if(charImage == null)
            {
                charImage = GetComponentsInChildren<Image>()[1];
            }
        }
    }
}