using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerManager : MonoBehaviour
{
    [SerializeField] private VideoClip _video;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private string _sceneToLoad;
    private VideoPlayer _videoPlayer;

    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }
    void Start()
    {
        _videoPlayer.clip = _video;
        _videoPlayer.loopPointReached += OnVideoFinished;
        _videoPlayer.playbackSpeed = _speed;

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
