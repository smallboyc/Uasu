using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] private VideoClip _video;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private string _sceneToLoad;
    private VideoPlayer _videoPlayer;
    private AudioSource _audioSource;
    private bool _canPlayOnPause;

    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        _videoPlayer.clip = _video;
        _audioSource.volume = SoundManager.Instance.GetVolume();
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        _videoPlayer.loopPointReached += OnVideoFinished;
        _videoPlayer.playbackSpeed = _speed;
        _videoPlayer.Play();

        //little trick to hide the cliping video eheh,
        StartCoroutine(RemovePanels());
    }

    private IEnumerator RemovePanels()
    {
        yield return new WaitForSeconds(2.0f);
        UIManager.Instance.HideAllExcept(PanelType.Video);
    }



    void Update()
    {
        if (UIManager.Instance.GamePaused)
        {
            _canPlayOnPause = true;
            _videoPlayer.Pause();
        }

        if (!UIManager.Instance.GamePaused && _canPlayOnPause)
        {
            _canPlayOnPause = false;
            _audioSource.volume = SoundManager.Instance.GetVolume();
            _videoPlayer.Play();
        }


        if (Input.GetKey(KeyCode.P))
        {
            _videoPlayer.Stop();
            SceneManager.LoadScene(_sceneToLoad);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

}
