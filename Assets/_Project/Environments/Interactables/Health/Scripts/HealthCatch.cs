using UnityEngine;

public class HealthCatch : MonoBehaviour
{
    private bool _playerInRange;

    //Sounds
    [Header("Sounds")]
    public AudioClip GainHealth;
    public ParticleSystem HealthParticles;

    void Update()
    {

        if (!_playerInRange)
            return;

        if (PlayerInputManager.Instance.InteractPressed)
        {
            if (SoundManager.Instance)
                SoundManager.Instance.PlaySoundClip(GainHealth, transform);
            PlayerManager.Instance.HealthManager.Heal();
            PlayHealthParticles();
            UIManager.Instance.Hide(PanelType.Help);
            Destroy(gameObject);
        }
    }


    // Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            HelpManager.Instance.SetHelpText("INTERACT to get health");
            UIManager.Instance.Show(PanelType.Help);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            UIManager.Instance.Hide(PanelType.Help);
        }
    }

    void PlayHealthParticles()
    {
        ParticleSystem particles = Instantiate(HealthParticles, transform.position, Quaternion.identity);
        particles.Play();
        Destroy(particles.gameObject, particles.main.duration);
    }


}
