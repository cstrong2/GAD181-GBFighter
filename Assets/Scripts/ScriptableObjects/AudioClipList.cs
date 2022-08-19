using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "_AudioClips", menuName = "Sounds/New Audio Clip List", order = +1)]

    public class AudioClipList : ScriptableObject
    {
        [SerializeField] private List<AudioClip> audioClips = new();
    
        public AudioClip GetRandomClip()
        {
            if (audioClips == null)
                return null;
            
            AudioClip clip = audioClips[Random.Range(0, audioClips.Count)];
            return clip;
        }
    }
}
