using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerLocomotionManager : CharacterLocomotionManager
{
    private PlayerManager _playerManager;
    private Transform _cameraTransform;
    private float _currentSpeed = 0.0f;
    [SerializeField] private float _walkingSpeed = 3.0f;
    [SerializeField] private float _rotationSpeed = 6.0f;
    private Vector3 _moveDirection; //free move direction vector (magenta gizmos ---o)
    [SerializeField] private float _verticalVelocity = 0.0f;
    [SerializeField] private float _moveIntensity;


    public float GetIntensity
    {
        get
        {
            return _moveIntensity;
        }
    }

    public Vector3 GetMoveDirection
    {
        get
        {
            return _moveDirection;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return _playerManager._characterController.isGrounded;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _cameraTransform = Camera.main.transform;
        _playerManager = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        HandleGroundedMovement();
        FreePlayerRotation();

        Vector3 move = _moveDirection * _currentSpeed * Time.deltaTime;
        move.y = _verticalVelocity * Time.deltaTime;
        _playerManager._characterController.Move(move);
    }

    private void HandleGroundedMovement()
    {
        Vector2 input = new(
            PlayerInputManager.Instance._horizontalInput,
            PlayerInputManager.Instance._verticalInput
        );

        //Our camera has its own local coord system. We need to project our player input from this local system to the world.
        //Math => Rworld = Rlocal * input (mat product)
        Vector3 cameraForward = _cameraTransform.forward; //z axis in Unity
        Vector3 cameraRight = _cameraTransform.right; //x axis
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        _moveDirection = (cameraForward * input.y + cameraRight * input.x).normalized;

        _moveIntensity = input.magnitude;


        //TODO : We're using the running speed as normal speed for the moment.
        //Let's define more precisely in the future if we really want a sprint option.
        _currentSpeed = _walkingSpeed;


    }


    private void FreePlayerRotation()
    {
        if (_moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_moveDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }

    //Cool little gizmos to show the Forward player direction.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Vector3 start = transform.position + Vector3.up;
        Vector3 end = start + _moveDirection * 2.0f;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.1f);
    }

}
