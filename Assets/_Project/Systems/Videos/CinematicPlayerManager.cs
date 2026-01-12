using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SceneLoader))]
public class CinematicPlayerManager : MonoBehaviour
{
    public static CinematicPlayerManager Instance;
    [SerializeField] private VideoClip _video;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private bool _isEndGame;

    private VideoPlayer _videoPlayer;
    private AudioSource _audioSource;

    public void PausePlayer()
    {
        _videoPlayer.Pause();
    }

    public void StartPlayer()
    {
        if (SoundManager.Instance)
            _audioSource.volume = SoundManager.Instance.GetMusic();
        _videoPlayer.Play();
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.StopMusic();

        _videoPlayer.clip = _video;
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        _videoPlayer.loopPointReached += OnVideoFinished;
        _videoPlayer.playbackSpeed = _speed;
        StartPlayer();

        //little trick to hide the cliping video eheh,
        StartCoroutine(RemovePanels());
    }

    private IEnumerator RemovePanels()
    {
        yield return new WaitForSeconds(2.0f);
        UIManager.Instance.HideAllExcept(PanelType.Cinematic);
    }



    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            _videoPlayer.Stop();
            if (_isEndGame)
            {
                GetComponent<SceneLoader>().QuitGameToMainMenu("Main");
                return;
            }
            SceneManager.LoadScene(_sceneToLoad);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        if (_isEndGame)
        {
            GetComponent<SceneLoader>().QuitGameToMainMenu("Main");
            return;
        }
        SceneManager.LoadScene(_sceneToLoad);
    }

}
