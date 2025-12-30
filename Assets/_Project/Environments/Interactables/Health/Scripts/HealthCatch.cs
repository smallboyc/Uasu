using UnityEngine;

public class HealthCatch : MonoBehaviour
{
    private bool _playerInRange;

    //Sounds
    [Header("Sounds")]
    public AudioClip GainHealth;

    void Update()
    {
        if (!_playerInRange)
            return;

        if (PlayerInputManager.Instance.InteractPressed)
        {
            SoundManager.Instance.PlaySoundClip(GainHealth, transform);
            PlayerManager.Instance.HealthManager.Heal();
            Destroy(gameObject);
        }
    }


    // Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }

    

}
