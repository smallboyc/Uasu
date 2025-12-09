using UnityEngine;

public class HealthCatch : MonoBehaviour
{
    private bool _playerInRange;

    void Update()
    {
        if (!_playerInRange)
            return;

        if (PlayerInputManager.Instance.InteractPressed)
        {
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
