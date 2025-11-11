using UnityEngine;

[RequireComponent(typeof(SorcererAnimationManager))]
[RequireComponent(typeof(SorcererSleepManager))]
public class SorcererManager : CharacterManager
{
    // State Machine
    public StateMachine SorcererStateMachine;

    // States
    private SorcererSleepState _sleepState;
    private SorcererWakeUpState _wakeUpState;

    // State Getter 
    public SorcererSleepState SleepState => _sleepState;
    public SorcererWakeUpState WakeUpState => _wakeUpState;

    // Manager
    [HideInInspector] public SorcererAnimationManager AnimationManager;
    [HideInInspector] public SorcererSleepManager SleepManager;

    protected override void Awake()
    {
        base.Awake();
        // Manager
        AnimationManager = GetComponent<SorcererAnimationManager>();
        SleepManager = GetComponent<SorcererSleepManager>();

        // States
        _sleepState = new SorcererSleepState(this);
        _wakeUpState = new SorcererWakeUpState(this);
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
