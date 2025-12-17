using UnityEngine;

public class StartMenuMusic : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;

    void Start()
    {
        // Reproduce la música en el centro de la escena
        SoundManager.Instance.PlaySoundClip(menuMusic, transform);
    }
}