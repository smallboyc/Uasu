using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private int _soulsRequired = 4;

    private bool _playerInRange;
    private bool _doorOpened;
    private Animator _animator;

    [Header("Sounds")]
    public AudioClip DoorOpening;

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

        // Interacci�n con la puerta
        _doorOpened = true;

        // Ocultar ayuda
        UIManager.Instance.Hide(PanelType.Help);

        // Activar animaci�n (igual que PressurePlateAction)
        if (_animator)
            _animator.SetBool("IsOpen", true);

        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(DoorOpening, transform);
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
            HelpManager.Instance.SetHelpText($"{_soulsRequired} souls...");

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