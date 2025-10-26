using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerManager : CharacterManager
{
    private PlayerLocomotionManager _playerLocomotionManager;
    private PlayerLockManager _playerLockManager;
    private PlayerAnimationManager _playerAnimationManager;
    private bool _isLockedOnEnemy;



    protected override void Awake()
    {
        base.Awake();
        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        _playerLockManager = GetComponent<PlayerLockManager>();
        _playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    protected override void Update()
    {
        base.Update();
        _playerLockManager.TargetLockEnemies(ref _isLockedOnEnemy);
        _playerLocomotionManager.HandleAllMovement(characterController, _playerLockManager, _isLockedOnEnemy);
        _playerAnimationManager.HandlePlayerAnimations(characterController, _playerLocomotionManager, _isLockedOnEnemy);
    }
}

