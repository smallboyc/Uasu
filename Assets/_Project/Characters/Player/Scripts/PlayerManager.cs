using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerManager : CharacterManager
{
    private PlayerLocomotionManager _playerLocomotionManager;
    private PlayerLockManager _playerLockManager;
    private PlayerAnimationManager _playerAnimationManager;

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
        _playerLockManager.TargetLockEnemies();
        _playerLocomotionManager.HandleAllMovement(characterController, _playerLockManager);
        _playerAnimationManager.HandlePlayerAnimations(characterController, _playerLocomotionManager, _playerLockManager.IsLockedOnEnemy);
    }
}

