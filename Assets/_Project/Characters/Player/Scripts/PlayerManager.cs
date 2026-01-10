using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint
{
    public Vector3 position;
    public string scene;
}

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
    public static PlayerManager Instance => _instance;
    // State Machine
    public StateMachine PlayerStateMachine;
    private PlayerSleepState _sleepState;
    private PlayerIdleState _idleState;
    private PlayerWalkState _walkState;
    private PlayerAerialState _aerialState;
    private PlayerLockState _lockState;
    private PlayerAttackState _attackState;
    private PlayerHurtState _hurtState;
    private PlayerPushState _pushState;
    private PlayerDeathState _deathState;


    // State Getter
    public PlayerSleepState SleepState => _sleepState;
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

    //Player Can Start play
    public bool IsPlayerActive;

    public void ActivePlayer()
    {
        IsPlayerActive = true;
    }

    //Transition between scenes
    public bool IsTransitioning;

    //Sounds
    [Header("Sounds")]
    public AudioClip[] AttackSounds;
    public AudioClip LockSounds;
    public AudioClip WalkSounds;
    public AudioClip HurtSounds;
    public AudioClip JumpSounds;
    public AudioClip WakingUpSounds;
    public AudioClip PushSounds;


    //Checkpoint
    public Checkpoint Checkpoint = new();

    //Inventory
    [HideInInspector] public enum CollectableItems { Sword, Lever, Health }
    public Dictionary<CollectableItems, GameObject> Collectables = new();
    public Transform HandHolder;
    public Transform BackHolder;
    public bool HoldSword;

    private void GetHolders()
    {
        if (HandHolder == null)
            HandHolder = GameObject.FindGameObjectWithTag("HandHolder").transform;
        if (BackHolder == null)
            BackHolder = GameObject.FindGameObjectWithTag("BackHolder").transform;
    }


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

        DontDestroyOnLoad(gameObject);

        // Manager
        AnimationManager = GetComponent<PlayerAnimationManager>();
        LocomotionManager = GetComponent<PlayerLocomotionManager>();
        LockManager = GetComponent<PlayerLockManager>();
        AttackManager = GetComponent<PlayerAttackManager>();
        HealthManager = GetComponent<PlayerHealthManager>();
        CollisionManager = GetComponent<PlayerCollisionManager>();

        // States
        _sleepState = new PlayerSleepState();
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
        PlayerStateMachine.Initialize(_sleepState);
        // AddAchievement("THE_SORCERER_FLOWER");
        if (IsometricCameraManager.Instance)
            IsometricCameraManager.Instance.IsometricCamera.Follow = transform;
        if (PlayerHealthBarManager.Instance)
            PlayerHealthBarManager.Instance.SetMaxHealth(HealthManager.Health);
        GetHolders();
        Checkpoint.position = transform.position;
        Checkpoint.scene = "Level_01_Main";
    }

    protected override void Update()
    {
        base.Update();
        if (!DialogueManager.Instance)
            return;

        if (UIManager.Instance && UIManager.Instance.IsLoading)
            return;

        if (UIManager.Instance.GamePaused && PlayerStateMachine.CurrentState != _sleepState)
        {
            PlayerStateMachine.ChangeState(_idleState);
            return;
        }

        if (Instance.CharacterController.enabled)
            PlayerStateMachine.CurrentState.Update();
    }
}

