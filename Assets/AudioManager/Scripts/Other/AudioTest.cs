using UnityEngine;
using UnityEngine.UI;
using AudioController;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class AudioTest : MonoBehaviour
{
    public AudioClip loaded;
    public AudioClip buttonClick;
    public AudioClip explosion;
    public AudioClip music;

    public Button play2DButton;
    public Button play3DButton;
    public Button playMusicButton;
    public Button stopMusicButton;
    public Button reloadSceneButton;

    public Toggle generalToggle;
    public Toggle musicToggle;
    public Toggle sfxToggle;

    public Slider generalVolume;
    public Slider musicVolume;
    public Slider sfxVolume;

    private AudioSource _musicSource;

    private void Awake()
    {
        play2DButton.onClick.AddListener(Play2DClicked);
        play3DButton.onClick.AddListener(Play3DClicked);
        playMusicButton.onClick.AddListener(PlayMusicClicked);
        stopMusicButton.onClick.AddListener(StopMusicClicked);
        reloadSceneButton.onClick.AddListener(ReloadScene);

        generalToggle.onValueChanged.AddListener(delegate(bool isActive) { Toggle(MixerChannel.Master, isActive); });
        musicToggle.onValueChanged.AddListener(delegate(bool isActive) { Toggle(MixerChannel.Music, isActive); });
        sfxToggle.onValueChanged.AddListener(delegate(bool isActive) { Toggle(MixerChannel.Sounds, isActive); });
        
        generalVolume.onValueChanged.AddListener(delegate(float volume) { SetVolume(MixerChannel.Master, volume); });
        musicVolume.onValueChanged.AddListener(delegate(float volume) { SetVolume(MixerChannel.Music, volume); });
        sfxVolume.onValueChanged.AddListener(delegate(float volume) { SetVolume(MixerChannel.Sounds, volume); });

        stopMusicButton.enabled = false;
    }

    private void Play2DClicked()
    {
        AudioManager.Instance.PlaySound2D(buttonClick);
        AudioManager.Instance.PlaySound2D(loaded);
    }

    private void Play3DClicked()
    {
        AudioManager.Instance.PlaySound2D(buttonClick);
        AudioManager.Instance.PlaySound3D(explosion,
            new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f), Random.Range(-2.5f, 2.5f)));
    }

    private void PlayMusicClicked()
    {
        AudioManager.Instance.PlaySound2D(buttonClick);

        if (_musicSource == null)
        {
            _musicSource = AudioManager.Instance.PlayMusic(music);
            playMusicButton.enabled = false;
            stopMusicButton.enabled = true;
        }
    }

    private void StopMusicClicked()
    {
        AudioManager.Instance.PlaySound2D(buttonClick);

        if (_musicSource != null)
        {
            AudioManager.Instance.StopMusic(_musicSource);
            playMusicButton.enabled = true;
            stopMusicButton.enabled = false;
            _musicSource = null;
        }
    }

    private void Toggle(MixerChannel channel, bool isActive)
    {
        if (isActive)
        {
            AudioManager.Instance.SetMixerVolume(channel, 0);
        }
        else
        {
            AudioManager.Instance.SetMixerVolume(channel, -80f);
        }
    }

    private void SetVolume(MixerChannel channel, float volume)
    {
        AudioManager.Instance.SetMixerVolume(channel, volume);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("AudioControllerTemplate");
    }
}