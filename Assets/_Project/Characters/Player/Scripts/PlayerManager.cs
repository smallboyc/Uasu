using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAttackManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(PlayerHurtManager))]
public class PlayerManager : CharacterManager
{
    // State Machine
    public StateMachine PlayerStateMachine;
    private PlayerIdleState _idleState;
    private PlayerWalkState _walkState;
    private PlayerAerialState _aerialState;
    private PlayerLockState _lockState;
    private PlayerAttackState _attackState;
    private PlayerHurtState _hurtState;


    // State Getter
    public PlayerIdleState IdleState => _idleState;
    public PlayerWalkState WalkState => _walkState;
    public PlayerAerialState AerialState => _aerialState;
    public PlayerLockState LockState => _lockState;
    public PlayerAttackState AttackState => _attackState;
    public PlayerHurtState HurtState => _hurtState;

    // Manager
    [HideInInspector] public PlayerAnimationManager AnimationManager;
    [HideInInspector] public PlayerLocomotionManager LocomotionManager;
    [HideInInspector] public PlayerAttackManager AttackManager;
    [HideInInspector] public PlayerLockManager LockManager;
    [HideInInspector] public PlayerHurtManager HurtManager;

    [Header("Attack")]
    [SerializeField] private float _attackCooldown = 1.0f;
    private bool _canAttack = true;
    [HideInInspector] public bool CanAttack => _canAttack;

    public IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    [HideInInspector]
    public int Health
    {
        get => _health;
        set => _health = value;
    }

    protected override void Awake()
    {
        base.Awake();
        // Manager
        AnimationManager = GetComponent<PlayerAnimationManager>();
        LocomotionManager = GetComponent<PlayerLocomotionManager>();
        LockManager = GetComponent<PlayerLockManager>();
        AttackManager = GetComponent<PlayerAttackManager>();
        HurtManager = GetComponent<PlayerHurtManager>();

        // States
        _idleState = new PlayerIdleState(this);
        _walkState = new PlayerWalkState(this);
        _aerialState = new PlayerAerialState(this);
        _lockState = new PlayerLockState(this);
        _attackState = new PlayerAttackState(this);
        _hurtState = new PlayerHurtState(this);
    }
    protected override void Start()
    {
        base.Start();
        // State Machine
        PlayerStateMachine = new StateMachine();
        PlayerStateMachine.Initialize(_idleState);
    }

    protected override void Update()
    {
        base.Update();
        PlayerStateMachine.CurrentState.Update();
    }
}

