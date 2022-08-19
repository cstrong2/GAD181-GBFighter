using Events;
using UnityEngine;

namespace Audio
{
    public class AudioClipReceiver : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        [Range(0,1)]
        [SerializeField]
        private float volume;
        
        private void OnValidate()
        {
            GetAudioSource();
            ConfigureAudioSource();
        }

        private void OnEnable()
        {
            GameEvents.OnAudioCollisionEvent += PlayClip;
        }
        
        private void OnDisable()
        {
            GameEvents.OnAudioCollisionEvent -= PlayClip;
        }

        private void PlayClip(AudioClip clip)
        {
//            Debug.Log("Play the clip " + clip);
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
            else
            {
                GetAudioSource();
                audioSource.PlayOneShot(clip);
            }
        }

        void GetAudioSource()
        {
            if (audioSource != null)
            {
                return;
            } 
            audioSource = this.gameObject.AddComponent<AudioSource>();
            ConfigureAudioSource();
            Debug.Log("We added a new AudioSource");
        }

        void ConfigureAudioSource()
        {
            audioSource.volume = volume;
        }
    }
}