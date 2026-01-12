using UnityEngine;

public class CastleDoorManager : MonoBehaviour
{
    [Header("Souls")]
    [SerializeField] private int _soulsRequired = 4;

    [Header("Interaction")]
    [SerializeField] private KeyCode _interactKey = KeyCode.E;

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

        if (PlayerManager.Instance.SoulCounter >= _soulsRequired)
        {
            if (Input.GetKeyDown(_interactKey))
            {
                Interact();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _playerInRange = true;

        if (PlayerManager.Instance.SoulCounter >= _soulsRequired)
        {
            HelpManager.Instance.SetHelpText("INTERACT");
        }
        else
        {
            HelpManager.Instance.SetHelpText("You need more souls");
        }

        UIManager.Instance.Show(PanelType.Help);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        _playerInRange = false;
        UIManager.Instance.Hide(PanelType.Help);
    }

    private void Interact()
    {
        _doorOpened = true;

        // Ocultar ayuda
        UIManager.Instance.Hide(PanelType.Help);

        // Activar animación (misma que PressurePlateAction)
        if (_animator)
        {
            _animator.SetBool("IsOpen", true);
        }

        // Opcional: consumir souls
        // PlayerManager.Instance.SoulCounter -= _soulsRequired;
    }
}