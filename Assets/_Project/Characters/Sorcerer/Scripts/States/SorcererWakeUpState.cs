using UnityEngine;

public class SorcererWakeUpState : SorcererState
{
    public SorcererWakeUpState(SorcererManager sorcererManager) : base(sorcererManager) { }

    public override void Enter()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(_sorcererManager.WakeUpSound, _sorcererManager.transform);
        _sorcererManager.AnimationManager.PlayWakeUpAnimation();
        _sorcererManager.SleepManager.StartSleepCountdownCoroutine();
    }

    public override void Update()
    {
        // Sorcerer Fall asleep?
        if (_sorcererManager.SleepManager.IsSleeping)
        {
            _sorcererManager.SorcererStateMachine.ChangeState(_sorcererManager.SleepState);
        }


        // Dialogues //
        if (!_sorcererManager.DialogueTrigger.PlayerCanDialogue())
            return;

        if (PlayerInputManager.Instance.InteractPressed)
        {
            _sorcererManager.SleepManager.StopSleepCountdownCoroutine();
            _sorcererManager.SorcererStateMachine.ChangeState(_sorcererManager.DialogueState);
        }
    }

    public override void Exit()
    {
    }

}
