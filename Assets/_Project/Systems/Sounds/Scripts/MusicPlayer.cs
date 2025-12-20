using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    void Start()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlayMusic(music);
    }
}