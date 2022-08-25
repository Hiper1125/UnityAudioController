using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace AudioController
{
    [RequireComponent(typeof(AudioListener))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private GameObject refSound2D;
        [SerializeField] private GameObject refSound3D;
        
        public static AudioManager Instance { get; private set; }
        
#if UNITY_EDITOR
        [SerializeField] private AudioClip loadedSFX;
        
        private void Start()
        {
            PlaySound2D(loadedSFX);
        }
#endif
        private void Awake()
        {
            SceneManager.activeSceneChanged += delegate(Scene newScene, Scene scene) { CleanAudioListeners(); };
            
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }

            CleanAudioListeners();
        }
        
        private void CleanAudioListeners()
        {
            var listeners = FindObjectsOfType<AudioListener>();

            if (listeners.Length >= 1)
            {
                foreach (var listener in listeners)
                {
                    if(listener.gameObject.GetComponent<AudioManager>() != null) continue;
                
                    DestroyImmediate(listener);
                }
            }
        }
        
        public void PlaySound2D(AudioClip audioClip, float volume = 1f)
        {
            var sound2D = Instantiate(refSound2D, Vector3.zero, Quaternion.identity, transform).GetComponent<AudioSource>();
            sound2D.clip = audioClip;
            sound2D.volume = volume;
            sound2D.outputAudioMixerGroup = mixer.FindMatchingGroups("Sounds")[0];

            sound2D.Play();
            DeleteSource(sound2D, audioClip.length);
        }

        public void PlaySound3D(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            var sound3D = Instantiate(refSound3D, position, Quaternion.identity).GetComponent<AudioSource>();
            sound3D.clip = audioClip;
            sound3D.volume = volume;
            sound3D.outputAudioMixerGroup = mixer.FindMatchingGroups("Sounds")[0];
            
            sound3D.Play();
            DeleteSource(sound3D, audioClip.length);
        }

        public AudioSource PlayMusic(AudioClip audioClip, float volume = 1f)
        {
            var sound2D = Instantiate(refSound2D, Vector3.zero, Quaternion.identity, transform).GetComponent<AudioSource>();
            sound2D.clip = audioClip;
            sound2D.volume = volume;
            sound2D.loop = true;
            sound2D.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];

            sound2D.Play();

            return sound2D;
        }

        public void StopMusic(AudioSource musicSource)
        {
            DeleteSource(musicSource);
        }

        public void SetMixerVolume(MixerChannel channel, float volume)
        {
            switch (channel)
            {
                case MixerChannel.Master: mixer.SetFloat("MasterVolume", volume); break;
                case MixerChannel.Sounds: mixer.SetFloat("SoundsVolume", volume); break;
                case MixerChannel.Music: mixer.SetFloat("MusicVolume", volume); break;
            }
        }

        private void DeleteSource(AudioSource source, float delay = 0f)
        {
            Destroy(source.gameObject, delay);
        }
    }
}

