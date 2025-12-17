using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource _audioSourcePrefab;

    [SerializeField] private AudioSource musicSource;

    public Slider volumeSlider;
    public float GetVolume()
    {
        return volumeSlider.value;
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
        musicSource.volume = volumeSlider.value;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
