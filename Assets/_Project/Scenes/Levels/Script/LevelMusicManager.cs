using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip levelMusic;

    void Start()
    {
        if (levelMusic == null) return;

        SoundManager.Instance.PlaySoundClip(levelMusic, transform);
    }
}