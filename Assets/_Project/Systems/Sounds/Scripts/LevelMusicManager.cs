using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic;

    void Start()
    {
        SoundManager.Instance.PlayMusic(levelMusic);
    }
}