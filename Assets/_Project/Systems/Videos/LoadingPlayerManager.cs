using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class LoadingPlayerManager : MonoBehaviour
{
    private VideoPlayer _videoPlayer;

    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
    {
        _videoPlayer.Play();
    }

    void OnDisable()
    {
        _videoPlayer.Stop();
    }
}
