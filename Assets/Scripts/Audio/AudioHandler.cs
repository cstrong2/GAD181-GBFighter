
using Events;
using ScriptableObjects;
using UnityEngine;

namespace Audio
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] public AudioClipList audioClipListData;

        private void OnCollisionEnter(Collision collision)
        {
            GameEvents.OnAudioCollisionEvent?.Invoke(audioClipListData.GetRandomClip());
        }
    }
}