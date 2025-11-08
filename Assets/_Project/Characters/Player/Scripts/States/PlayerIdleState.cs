
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerManager playerManager, PlayerAnimationManager animationManager)
        : base(playerManager, animationManager)
    {
        Priority = 1;
    }

    public override void Enter()
    {
        // _animationManager.PlayIdleAnimation();
        Debug.Log("Enter Idle State");
    }


    public override void Update()
    {
        // idle logic
    }

    public override void Exit()
    {
        // clean
    }
}

