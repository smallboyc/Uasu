using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAttackManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(PlayerHealthManager))]
public class PlayerManager : CharacterManager
{
    // State Machine
    public StateMachine PlayerStateMachine;
    public PlayerIdleState _idleState;
    public PlayerWalkState _walkState;
    public PlayerAerialState _aerialState;
    public PlayerAttackState _attackState;
    public PlayerHurtState _hurtState;


    // State Getter
    public PlayerIdleState IdleState => _idleState;
    public PlayerWalkState WalkState => _walkState;
    public PlayerAerialState AerialState => _aerialState;
    public PlayerAttackState AttackState => _attackState;
    public PlayerHurtState HurtState => _hurtState;

    // Manager
    public PlayerAnimationManager AnimationManager;
    public PlayerLocomotionManager LocomotionManager;
    public PlayerAttackManager AttackManager;
    public PlayerLockManager LockManager;
    // private PlayerHealthManager _playerHealthManager;

    [SerializeField] private float _attackCooldown = 1.0f;
    private bool _canAttack = true;
    [HideInInspector] public bool CanAttack => _canAttack;

    public IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    protected override void Awake()
    {
        base.Awake();
        // Manager
        AnimationManager = GetComponent<PlayerAnimationManager>();
        LocomotionManager = GetComponent<PlayerLocomotionManager>();
        LockManager = GetComponent<PlayerLockManager>();
        AttackManager = GetComponent<PlayerAttackManager>();

        // States
        _idleState = new PlayerIdleState(this);
        _walkState = new PlayerWalkState(this);
        _aerialState = new PlayerAerialState(this);
        _attackState = new PlayerAttackState(this);
        _hurtState = new PlayerHurtState(this);

        // State Machine
        PlayerStateMachine = new StateMachine();
        PlayerStateMachine.Initialize(_idleState);


        // _playerHealthManager = GetComponent<PlayerHealthManager>();




        // // Get Feature Component

        // _playerLockManager = GetComponent<PlayerLockManager>();
        // _playerAttackManager = GetComponent<PlayerAttackManager>();
        // _playerHealthManager = GetComponent<PlayerHealthManager>();
    }

    protected override void Update()
    {
        base.Update();
        PlayerStateMachine.CurrentState.Update();

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

