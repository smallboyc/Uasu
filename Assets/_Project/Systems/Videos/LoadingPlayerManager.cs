using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class LoadingPlayerManager : MonoBehaviour
{
    public static LoadingPlayerManager Instance;
    private VideoPlayer _videoPlayer;
    public float LoadingDuration = 3.0f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _videoPlayer = GetComponent<VideoPlayer>();
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
