using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ColorData_", menuName = "Colors/New Color", order = 0)]
    public class ColorData : ScriptableObject
    {
        public Color color;
    }
}
