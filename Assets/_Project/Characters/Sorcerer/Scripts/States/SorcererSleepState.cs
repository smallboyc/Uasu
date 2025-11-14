using UnityEngine;

public class SorcererSleepState : SorcererState
{
    public SorcererSleepState(SorcererManager sorcererManager) : base(sorcererManager) { }

    public override void Enter()
    {
        _sorcererManager.AnimationManager.PlaySleepAnimation();
    }

    public override void Update()
    {
        if (!_sorcererManager.SleepManager.IsSleeping)
        {
            _sorcererManager.SorcererStateMachine.ChangeState(_sorcererManager.WakeUpState);
        }
    }

    public override void Exit()
    {
    }
}
