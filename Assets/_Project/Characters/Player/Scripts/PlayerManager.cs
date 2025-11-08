using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAttackManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(PlayerHealthManager))]
public class PlayerManager : CharacterManager
{
    // State Machine
    private StateMachine _playerStateMachine;
    private PlayerIdleState _idleState;
    private PlayerWalkState _walkState;
    private PlayerAttackState _attackState;
    private PlayerHurtState _hurtState;


    // Manager
    // private PlayerLocomotionManager _playerLocomotionManager;
    // private PlayerLockManager _playerLockManager;
    // private PlayerAttackManager _playerAttackManager;
    // private PlayerHealthManager _playerHealthManager;
    private PlayerAnimationManager _playerAnimationManager;

    protected override void Awake()
    {
        base.Awake();
        _playerAnimationManager = GetComponent<PlayerAnimationManager>();

        // State Machine
        _playerStateMachine = new StateMachine();
        
        // States
        _idleState = new PlayerIdleState(this, _playerAnimationManager);
        _walkState = new PlayerWalkState(this, _playerAnimationManager);
        _attackState = new PlayerAttackState(this, _playerAnimationManager);
        _hurtState = new PlayerHurtState(this, _playerAnimationManager);

        _playerStateMachine.Initialize(_idleState);




        // // Get Feature Component
        // _playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        // _playerLockManager = GetComponent<PlayerLockManager>();
        // _playerAttackManager = GetComponent<PlayerAttackManager>();
        // _playerHealthManager = GetComponent<PlayerHealthManager>();

        // // Set AnimationManager to Feature Component
        // _playerLocomotionManager.SetAnimationManager(_playerAnimationManager);
        // _playerAttackManager.SetAnimationManager(_playerAnimationManager);
    }

    protected override void Update()
    {
        // base.Update();

        // if (_playerHealthManager.Health <= 0)
        // {
        //     _playerHealthManager.Die();
        //     return;
        // }

        // if (!_playerHealthManager.IsStunned)
        // {
        //     _playerLockManager.TargetLockEnemies();
        //     _playerAttackManager.HandleAttack(characterController);
        //     _playerLocomotionManager.HandleAllMovement(characterController, _playerLockManager, _playerAttackManager);
        // }
        // else
        // {
        //     Debug.Log("Stun");
        //     _playerLocomotionManager.HandleKnockback(characterController);
        // }
    }
}

