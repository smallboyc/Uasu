using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _audioSourcePrefab;
    [SerializeField] private AudioSource musicSource;
    private AudioSource loopAudioSource;

    [HideInInspector] public bool ManageSoundOnPause;

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;

    public float GetMusic()
    {
        return musicSlider.value;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (SoundManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (UIManager.Instance.GamePaused)
        {
            ManageSoundOnPause = true;

            if (CinematicPlayerManager.Instance)
                CinematicPlayerManager.Instance.PausePlayer();
        }

        if (!UIManager.Instance.GamePaused && ManageSoundOnPause)
        {
            ManageSoundOnPause = false;
            if (CinematicPlayerManager.Instance)
                CinematicPlayerManager.Instance.StartPlayer();
        }

    }

    public void PlaySoundClip(AudioClip clip, Transform transform)
    {
        AudioSource audioSource = Instantiate(_audioSourcePrefab, transform.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volumeSlider.value;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioSource.clip.length);

    }

    public void PlayRandomSoundClip(AudioClip[] clipList, Transform transform)
    {
        int rand = Random.Range(0, clipList.Length);
        PlaySoundClip(clipList[rand], transform);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.Stop();

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = musicSlider.value;
        musicSource.Play();
    }

    public void AdjustMusic()
    {
        musicSource.volume = musicSlider.value;
    }


    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayLoopClip(AudioClip clip, Transform transform)
    {
        // Si ya hay un loop sonando, no crear otro
        if (loopAudioSource != null)
            return;

        loopAudioSource = Instantiate(_audioSourcePrefab, transform.position, Quaternion.identity);
        loopAudioSource.clip = clip;
        loopAudioSource.loop = true;
        loopAudioSource.volume = volumeSlider.value;
        loopAudioSource.Play();
    }

    public void StopLoopSound()
    {
        if (loopAudioSource == null)
            return;

        loopAudioSource.Stop();
        Destroy(loopAudioSource.gameObject);
        loopAudioSource = null;
    }

    public void AdjustLoopVolume()
    {
        if (loopAudioSource != null)
            loopAudioSource.volume = volumeSlider.value;
    }
}
