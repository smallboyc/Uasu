using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager _instance;
    private PlayerControls _playerControls;
    public float horizontalInput;
    public float verticalInput;
    public bool jumpPressed;
    public bool lockPressed;
    public bool interactPressed;


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

            //--MOVEMENT--//
            //WALK, RUN
            _playerControls.PlayerMovement.Movement.performed += OnMovePerformed;
            //JUMP
            _playerControls.PlayerMovement.Jump.performed += OnJumpPerformed;
            _playerControls.PlayerMovement.Jump.canceled += OnJumpCanceled;

            //--INTERACTION--//
            //LOCK (enemy)
            _playerControls.PlayerInteraction.Lock.performed += OnLockPerformed;
            _playerControls.PlayerInteraction.Lock.canceled += OnLockCanceled;
            //INTERACT (props, environment, sorcerer)
            _playerControls.PlayerInteraction.Interact.performed += OnInteractPerformed;
            _playerControls.PlayerInteraction.Interact.canceled += OnInteractCanceled;

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
        horizontalInput = input.ReadValue<Vector2>().x;
        verticalInput = input.ReadValue<Vector2>().y;
    }

    private void OnJumpPerformed(InputAction.CallbackContext input)
    {
        jumpPressed = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext input)
    {
        jumpPressed = false;
    }

    private void OnLockPerformed(InputAction.CallbackContext input)
    {
        lockPressed = true;
    }

    private void OnLockCanceled(InputAction.CallbackContext input)
    {
        lockPressed = false;
    }
    private void OnInteractPerformed(InputAction.CallbackContext input)
    {
        interactPressed = true;
    }

    private void OnInteractCanceled(InputAction.CallbackContext input)
    {
        interactPressed = false;
    }
}