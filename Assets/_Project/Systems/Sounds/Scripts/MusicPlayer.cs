using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    void Start()
    {
        SoundManager.Instance.PlayMusic(music);
    }
}