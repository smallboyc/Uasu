using UnityEngine;

[RequireComponent(typeof(SorcererAnimationManager))]
[RequireComponent(typeof(SorcererSleepManager))]
[RequireComponent(typeof(SorcererDialogueTrigger))]
public class SorcererManager : CharacterManager
{
    // State Machine
    public StateMachine SorcererStateMachine;

    // States
    private SorcererSleepState _sleepState;
    private SorcererWakeUpState _wakeUpState;
    private SorcererDialogueState _dialogueState;

    // State Getter 
    public SorcererSleepState SleepState => _sleepState;
    public SorcererWakeUpState WakeUpState => _wakeUpState;
    public SorcererDialogueState DialogueState => _dialogueState;

    // Manager
    [HideInInspector] public SorcererAnimationManager AnimationManager;
    [HideInInspector] public SorcererSleepManager SleepManager;

    // Trigger => inherit from another external system. (DialogueManager with Dialogue Trigger)
    [HideInInspector] public SorcererDialogueTrigger DialogueTrigger;

    //Sounds
    [Header("Sounds")]
    public AudioClip WakeUpSound;
    public AudioClip Interact;


    protected override void Awake()
    {
        base.Awake();
        // Manager
        AnimationManager = GetComponent<SorcererAnimationManager>();
        SleepManager = GetComponent<SorcererSleepManager>();

        //Dialogue Trigger
        DialogueTrigger = GetComponent<SorcererDialogueTrigger>();

        // States
        _sleepState = new SorcererSleepState(this);
        _wakeUpState = new SorcererWakeUpState(this);
        _dialogueState = new SorcererDialogueState(this);
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
