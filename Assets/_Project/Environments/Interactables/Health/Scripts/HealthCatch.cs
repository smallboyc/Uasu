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
            HelpManager.Instance.SetHelpText("<Interact> to get health");
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
}
