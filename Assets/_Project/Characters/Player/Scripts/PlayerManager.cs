using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAttackManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(PlayerHealthManager))]
public class PlayerManager : CharacterManager
{
    private PlayerLocomotionManager _playerLocomotionManager;
    private PlayerLockManager _playerLockManager;
    private PlayerAttackManager _playerAttackManager;
    private PlayerHealthManager _playerHealthManager;
    private PlayerAnimationManager _playerAnimationManager;

    protected override void Awake()
    {
        base.Awake();
        _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        _playerLockManager = GetComponent<PlayerLockManager>();
        _playerAttackManager = GetComponent<PlayerAttackManager>();
        _playerHealthManager = GetComponent<PlayerHealthManager>();
        _playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (_playerHealthManager.Health <= 0)
        {
            _playerHealthManager.Die();
            return;
        }

        if (!_playerHealthManager.IsStunned)
        {
            _playerLockManager.TargetLockEnemies();
            _playerAttackManager.HandleAttack(characterController);
            _playerLocomotionManager.HandleAllMovement(characterController, _playerLockManager, _playerAttackManager);
        }
        else
        {
            _playerLocomotionManager.HandleKnockback(characterController);
        }

        _playerAnimationManager.HandlePlayerAnimations(characterController, _playerLocomotionManager, _playerLockManager.IsLockedOnEnemy, _playerAttackManager);
    }
}

