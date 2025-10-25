using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager _instance;
    private PlayerControls _playerControls;
    public float _horizontalInput;
    public float _verticalInput;
    public bool _jumpPressed;


    public static PlayerInputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("No Instance found for PlayerInputManager.");
            }
            return _instance;
        }
    }

    void Awake()
    {
        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }


    //Bind once for all, the Controller listener.
    private void OnEnable()
    {
        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();
            //WALK
            _playerControls.PlayerMovement.Movement.performed += OnMovePerformed;
            //JUMP
            _playerControls.PlayerMovement.Jump.performed += OnJumpPerformed;
            _playerControls.PlayerMovement.Jump.canceled += OnJumpCanceled;
        }
        _playerControls.Enable();
    }

    //Unbind the listener at the end.
    private void OnDisable()
    {
        _playerControls?.Disable();
    }

    //Get the x & y from the Controller left stick
    private void OnMovePerformed(InputAction.CallbackContext input)
    {
        _horizontalInput = input.ReadValue<Vector2>().x;
        _verticalInput = input.ReadValue<Vector2>().y;
    }

    private void OnJumpPerformed(InputAction.CallbackContext input)
    {
        _jumpPressed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext input)
    {
        _jumpPressed = false;
    }
}