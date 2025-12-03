using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    // Singleton
    private static PlayerInputManager _instance;
    public static PlayerInputManager Instance => _instance;

    // Inputs
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool LockPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool ToggleWeaponPressed { get; private set; }
    public bool AttackPressed { get; private set; }

    private PlayerControls _playerControls;

    // --- Singleton Setup ---
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    // --- Input System Setup ---
    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            // Movement
            _playerControls.PlayerMovement.Movement.performed += OnMovementPerformed;
            _playerControls.PlayerMovement.Jump.performed += _ => JumpPressed = true;
            _playerControls.PlayerMovement.Jump.canceled += _ => JumpPressed = false;

            // Interaction
            _playerControls.PlayerInteraction.Lock.performed += _ => LockPressed = true;
            _playerControls.PlayerInteraction.Lock.canceled += _ => LockPressed = false;
            _playerControls.PlayerInteraction.Interact.performed += _ => InteractPressed = true;
            _playerControls.PlayerInteraction.Interact.canceled += _ => InteractPressed = false;
            _playerControls.PlayerInteraction.ToggleWeapon.performed += _ => ToggleWeaponPressed = true;
            _playerControls.PlayerInteraction.ToggleWeapon.canceled += _ => ToggleWeaponPressed = false;
            _playerControls.PlayerInteraction.Attack.performed += _ => AttackPressed = true;
            _playerControls.PlayerInteraction.Attack.canceled += _ => AttackPressed = false;
        }

        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls?.Disable();
    }

    // --- Input Callbacks ---
    private void OnMovementPerformed(InputAction.CallbackContext input)
    {
        HorizontalInput = input.ReadValue<Vector2>().x;
        VerticalInput = input.ReadValue<Vector2>().y;
    }
}
