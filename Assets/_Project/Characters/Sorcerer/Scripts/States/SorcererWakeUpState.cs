using UnityEngine;

public class SorcererWakeUpState : SorcererState
{
    public SorcererWakeUpState(SorcererManager sorcererManager) : base(sorcererManager) { }

    public override void Enter()
    {
        _sorcererManager.AnimationManager.PlayWakeUpAnimation();
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
        _sorcererManager.AnimationManager.StopWakeUpAnimation();
    }

}
