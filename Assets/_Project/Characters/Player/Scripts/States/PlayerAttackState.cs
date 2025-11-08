
public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerManager playerManager, PlayerAnimationManager animationManager)
        : base(playerManager, animationManager)
    {
        Priority = 1;
    }
    public override void Enter()
    {
        _animationManager.PlayAttackAnimation();
    }

    public override void Update()
    {
        // attack logic
    }

    public override void Exit()
    {
        // clean
    }
}
