using UnityEngine;

[RequireComponent(typeof(SorcererAnimationManager))]
public class SorcererManager : CharacterManager
{
    // State Machine
    public StateMachine SorcererStateMachine;

    // States
    private SorcererSleepState _sleepState;
    private SorcererWakeUpState _wakeUpState;
    private SorcererIdleState _idleState;

    // State Getter 
    public SorcererSleepState SleepState => _sleepState;
    public SorcererWakeUpState WakeUpState => _wakeUpState;
    public SorcererIdleState IdleState => _idleState;

    // Manager
    [HideInInspector] public SorcererAnimationManager AnimationManager;

    protected override void Awake()
    {
        base.Awake();
        // Manager
        AnimationManager = GetComponent<SorcererAnimationManager>();

        // States
        _sleepState = new SorcererSleepState(this);
        _wakeUpState = new SorcererWakeUpState(this);
        _idleState = new SorcererIdleState(this);
    }

    protected override void Start()
    {
        base.Start();
        SorcererStateMachine = new StateMachine();
        SorcererStateMachine.Initialize(SleepState);
    }

    protected override void Update()
    {
        base.Update();
        SorcererStateMachine.CurrentState.Update();
    }
}
