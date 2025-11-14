using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerLocomotionManager : CharacterLocomotionManager
{
    private Transform _cameraTransform;
    private float _currentSpeed = 0.0f;
    private Vector2 _input;
    [SerializeField] private float _walkingSpeed = 4.0f;
    [SerializeField] private float _gravity = -24.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    [SerializeField] private float _rotationSpeed = 6.0f;
    [SerializeField] float _jumpCooldown = 0.6f;
    [SerializeField] bool _jumpCanBePerformed = true;
    private bool _jumpButtonReleased = true;
    private Vector3 _moveDirection; //free move direction vector (magenta gizmos ---o)
    [SerializeField] private float _verticalVelocity = 0.0f;
    [SerializeField] private float _moveIntensity;

    public bool IsMoving => _input != Vector2.zero;
    public Vector3 GetMoveDirection => _moveDirection;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    public void HandleAllMovement(CharacterController characterController, PlayerLockManager playerLockManager)
    {
        HandleGroundedMovement();
        HandleAerialMovement(characterController);
        HandleRotationMovement(playerLockManager);

        Vector3 move = _moveDirection * _currentSpeed * Time.deltaTime;
        move.y = _verticalVelocity * Time.deltaTime;

        characterController.Move(move);
    }

    private void HandleGroundedMovement()
    {
        _input = new Vector2(PlayerInputManager.Instance.HorizontalInput, PlayerInputManager.Instance.VerticalInput);

        //Our camera has its own local coord system. We need to project our player input from this local system to the world.
        //Math => Rworld = Rlocal * input (mat product)
        Vector3 cameraForward = _cameraTransform.forward; //z axis in Unity
        Vector3 cameraRight = _cameraTransform.right; //x axis
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        _moveDirection = (cameraForward * _input.y + cameraRight * _input.x).normalized;

        _currentSpeed = _walkingSpeed;

    }

    private void HandleAerialMovement(CharacterController characterController)
    {
        if (characterController.isGrounded)
        {
            if (_verticalVelocity < 0f)
                _verticalVelocity = -2f;

            if (PlayerInputManager.Instance.JumpPressed && _jumpButtonReleased && _jumpCanBePerformed)
            {
                _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
                StartCoroutine(ReloadJump());
            }
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        _jumpButtonReleased = !PlayerInputManager.Instance.JumpPressed;
    }


    private void HandleRotationMovement(PlayerLockManager playerLockManager)
    {
        if (!playerLockManager.IsLockedOnEnemy)
        {
            FreePlayerRotation();
        }
        else
        {
            LockPlayerRotation(playerLockManager);
        }
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

    private void LockPlayerRotation(PlayerLockManager playerLockManager)
    {
        if (playerLockManager.LockDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerLockManager.LockDirection);
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

    //Cooldown before Jumping again.
    private IEnumerator ReloadJump()
    {
        _jumpCanBePerformed = false;
        yield return new WaitForSeconds(_jumpCooldown);
        _jumpCanBePerformed = true;
    }

}