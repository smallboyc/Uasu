using UnityEngine;

public class SorcererWakeUpState : SorcererState
{
    public SorcererWakeUpState(SorcererManager sorcererManager) : base(sorcererManager) { }

    public override void Enter()
    {
        _sorcererManager.AnimationManager.PlayWakeUpAnimation();
        _sorcererManager.SleepManager.StartSleepCountdown();
    }

    public override void Update()
    {
        if (_sorcererManager.SleepManager.IsSleeping)
        {
            _sorcererManager.SorcererStateMachine.ChangeState(_sorcererManager.SleepState);
        }
    }

    public override void Exit()
    {
    }

}
