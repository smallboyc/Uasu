using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private static PlayerInputManager _instance;
    private PlayerControls _playerControls;
    public float _horizontalInput;
    public float _verticalInput;


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
}