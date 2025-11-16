using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAttackManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(PlayerHealthManager))]
[RequireComponent(typeof(PlayerCollisionManager))]
public class PlayerManager : CharacterManager
{
    // Singleton => Singleplayer game
    static PlayerManager _instance;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ERROR (PlayerManager): No Instance found.");
            }
            return _instance;
        }
    }
    // State Machine
    public StateMachine PlayerStateMachine;
    private PlayerIdleState _idleState;
    private PlayerWalkState _walkState;
    private PlayerAerialState _aerialState;
    private PlayerLockState _lockState;
    private PlayerAttackState _attackState;
    private PlayerHurtState _hurtState;
    private PlayerPushState _pushState;
    private PlayerDeathState _deathState;


    // State Getter
    public PlayerIdleState IdleState => _idleState;
    public PlayerWalkState WalkState => _walkState;
    public PlayerAerialState AerialState => _aerialState;
    public PlayerLockState LockState => _lockState;
    public PlayerAttackState AttackState => _attackState;
    public PlayerHurtState HurtState => _hurtState;
    public PlayerPushState PushState => _pushState;
    public PlayerDeathState DeathState => _deathState;

    // Manager
    [HideInInspector] public PlayerAnimationManager AnimationManager;
    [HideInInspector] public PlayerLocomotionManager LocomotionManager;
    [HideInInspector] public PlayerAttackManager AttackManager;
    [HideInInspector] public PlayerLockManager LockManager;
    [HideInInspector] public PlayerHealthManager HealthManager;
    [HideInInspector] public PlayerCollisionManager CollisionManager;


    // Flags : used to determine all achievements unlocked 
    private HashSet<string> _achievements = new();

    public void AddAchievement(string flag)
    {
        _achievements.Add(flag);
    }

    public bool HasAchievement(string flag)
    {
        return _achievements.Contains(flag);
    }

    [Header("Souls")]
    public int SoulCounter;

    protected override void Awake()
    {
        base.Awake();

        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (PlayerManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        _instance = this;

        // Manager
        AnimationManager = GetComponent<PlayerAnimationManager>();
        LocomotionManager = GetComponent<PlayerLocomotionManager>();
        LockManager = GetComponent<PlayerLockManager>();
        AttackManager = GetComponent<PlayerAttackManager>();
        HealthManager = GetComponent<PlayerHealthManager>();
        CollisionManager = GetComponent<PlayerCollisionManager>();

        // States
        _idleState = new PlayerIdleState();
        _walkState = new PlayerWalkState();
        _aerialState = new PlayerAerialState();
        _lockState = new PlayerLockState();
        _attackState = new PlayerAttackState();
        _hurtState = new PlayerHurtState();
        _pushState = new PlayerPushState();
        _deathState = new PlayerDeathState();
    }
    protected override void Start()
    {
        base.Start();
        // State Machine
        PlayerStateMachine = new StateMachine();
        PlayerStateMachine.Initialize(_idleState);
        // AddAchievement("THE_SORCERER_FLOWER");
        PlayerHealthBarManager.Instance.SetHealth(HealthManager.Health);
    }

    protected override void Update()
    {
        base.Update();
        PlayerStateMachine.CurrentState.Update();
    }
}

