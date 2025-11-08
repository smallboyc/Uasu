
public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerManager playerManager, PlayerAnimationManager animationManager)
        : base(playerManager, animationManager)
    {
        Priority = 1;
    }


    public override void Enter()
    {
        // _animationManager.PlayWalkAnimation();
    }

    public override void Update()
    {
        // walk logic
    }

    public override void Exit()
    {
        // clean
    }
}
