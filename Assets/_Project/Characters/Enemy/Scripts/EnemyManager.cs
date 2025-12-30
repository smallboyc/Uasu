using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotionManager))]
[RequireComponent(typeof(EnemyLockManager))]
[RequireComponent(typeof(EnemyAttackManager))]
[RequireComponent(typeof(EnemyAnimationManager))]
[RequireComponent(typeof(EnemyHealthManager))]
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
    [HideInInspector] public EnemyHealthManager HealthManager;

    // Sounds
    [Header("Header")]
    public AudioClip HurtSound;
    public AudioClip AttackSound;

    [Header("Patrol")]
    [SerializeField] private List<Transform> _wayPoints;
    [HideInInspector] public List<Transform> WayPoints => _wayPoints;

    public void SetWayPoints(List<Transform> enemySpawnerWaypoints)
    {
        _wayPoints = enemySpawnerWaypoints;
    }

    protected override void Awake()
    {
        base.Awake();
        // Manager
        AnimationManager = GetComponent<EnemyAnimationManager>();
        LocomotionManager = GetComponent<EnemyLocomotionManager>();
        LockManager = GetComponent<EnemyLockManager>();
        AttackManager = GetComponent<EnemyAttackManager>();
        HealthManager = GetComponent<EnemyHealthManager>();

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
    
        if (UIManager.Instance && UIManager.Instance.GamePaused)
            
            return;

        
        base.Update();
        EnemyStateMachine.CurrentState.Update();

       
    }


}
