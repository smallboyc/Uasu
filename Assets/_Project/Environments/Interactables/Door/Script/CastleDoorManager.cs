using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private int _soulsRequired = 4;

    private bool _playerInRange;
    private bool _doorOpened;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_playerInRange || _doorOpened)
            return;

        if (!PlayerInputManager.Instance.InteractPressed)
            return;

        if (PlayerManager.Instance.SoulCounter < _soulsRequired)
            return;

        // Interacción con la puerta
        _doorOpened = true;

        // Ocultar ayuda
        UIManager.Instance.Hide(PanelType.Help);

        // Activar animación (igual que PressurePlateAction)
        if (_animator)
            _animator.SetBool("IsOpen", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _playerInRange = true;

        if (_doorOpened)
            return;

        if (PlayerManager.Instance.SoulCounter >= _soulsRequired)
            HelpManager.Instance.SetHelpText("INTERACT");
        else
            HelpManager.Instance.SetHelpText("You need more souls");

        UIManager.Instance.Show(PanelType.Help);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _playerInRange = false;
        UIManager.Instance.Hide(PanelType.Help);
    }
}