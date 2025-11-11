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
    }

    public override void Exit()
    {
        _sorcererManager.AnimationManager.StopSleepAnimation();
    }

}
