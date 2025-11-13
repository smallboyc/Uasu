using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerLocomotionManager))]
[RequireComponent(typeof(PlayerLockManager))]
[RequireComponent(typeof(PlayerAttackManager))]
[RequireComponent(typeof(PlayerAnimationManager))]
[RequireComponent(typeof(PlayerHurtManager))]
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
        HurtManager = GetComponent<PlayerHurtManager>();

        // States
        _idleState = new PlayerIdleState();
        _walkState = new PlayerWalkState();
        _aerialState = new PlayerAerialState();
        _lockState = new PlayerLockState();
        _attackState = new PlayerAttackState();
        _hurtState = new PlayerHurtState();
    }
    protected override void Start()
    {
        base.Start();
        // State Machine
        PlayerStateMachine = new StateMachine();
        PlayerStateMachine.Initialize(_idleState);
        AddAchievement("HasSorcererFlower");
    }

    protected override void Update()
    {
        base.Update();
        PlayerStateMachine.CurrentState.Update();
    }
}

