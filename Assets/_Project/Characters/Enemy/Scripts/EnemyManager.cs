using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotionManager))]
[RequireComponent(typeof(EnemyLockManager))]
[RequireComponent(typeof(EnemyAttackManager))]
[RequireComponent(typeof(EnemyAnimationManager))]
[RequireComponent(typeof(EnemyHurtManager))]
public class EnemyManager : CharacterManager
{

    // State Machine
    public StateMachine EnemyStateMachine;
    private EnemyIdleState _idleState;
    private EnemyPatrolState _patrolState;
    private EnemyFocusState _focusState;
    private EnemyAttackState _attackState;
    private EnemyHurtState _hurtState;
    private EnemyDeathState _deathState;


    // State Getter
    public EnemyIdleState IdleState => _idleState;
    public EnemyPatrolState PatrolState => _patrolState;
    public EnemyFocusState FocusState => _focusState;
    public EnemyAttackState AttackState => _attackState;
    public EnemyHurtState HurtState => _hurtState;
    public EnemyDeathState DeathState => _deathState;

    // Manager
    [HideInInspector] public EnemyAnimationManager AnimationManager;
    [HideInInspector] public EnemyLocomotionManager LocomotionManager;
    [HideInInspector] public EnemyLockManager LockManager;
    [HideInInspector] public EnemyAttackManager AttackManager;
    [HideInInspector] public EnemyHurtManager HurtManager;


    [Header("Attack")]
    [SerializeField] private float _attackCooldown = 2.0f;
    private bool _canAttack = true;
    [HideInInspector] public bool CanAttack => _canAttack;

    public IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    [Header("Patrol")]
    [SerializeField] private List<Transform> _wayPoints;
    [HideInInspector] public List<Transform> WayPoints => _wayPoints;

    public void SetWayPoints(List<Transform> enemySpawnerWaypoints)
    {
        _wayPoints = enemySpawnerWaypoints;
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
        AnimationManager = GetComponent<EnemyAnimationManager>();
        LocomotionManager = GetComponent<EnemyLocomotionManager>();
        LockManager = GetComponent<EnemyLockManager>();
        AttackManager = GetComponent<EnemyAttackManager>();
        HurtManager = GetComponent<EnemyHurtManager>();

        // States
        _idleState = new EnemyIdleState(this);
        _patrolState = new EnemyPatrolState(this);
        _focusState = new EnemyFocusState(this);
        _attackState = new EnemyAttackState(this);
        _hurtState = new EnemyHurtState(this);
        _deathState = new EnemyDeathState(this);
    }

    protected override void Start()
    {
        base.Start();
        // State Machine
        EnemyStateMachine = new StateMachine();
        EnemyStateMachine.Initialize(_patrolState);
    }

    protected override void Update()
    {
        base.Update();
        EnemyStateMachine.CurrentState.Update();
        // if (_enemyHealthManager.Health <= 0)
        // {
        //     _enemyHealthManager.GiveSoul();
        //     _enemyHealthManager.Die();
        //     return;
        // }

        // if (!_enemyHealthManager.IsStunned)
        // {
        //     _enemyAttackManager.HandleAttack(_enemyLockManager);

        //     if (!_enemyAttackManager.IsAttacking)
        //     {
        //         _enemyLockManager.TargetLockPlayer();
        //         _enemyLocomotionManager.HandleAllMovement(CharacterController, _wayPoints, _enemyLockManager);
        //     }
        // }
        // else
        // {
        //     _enemyLocomotionManager.HandleKnockback(CharacterController);
        // }
    }


}
