
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(PlayerManager playerManager, PlayerAnimationManager animationManager)
        : base(playerManager, animationManager)
    {
        Priority = 2;
    }

    public override void Enter()
    {
        // _animationManager.PlayHurtAnimation();
        Debug.Log("Enter Hurt State");
    }

    public override void Update()
    {
        // hurt logic
    }

    public override void Exit()
    {
        // clean
    }
}
