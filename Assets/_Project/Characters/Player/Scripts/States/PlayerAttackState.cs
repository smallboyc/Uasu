
public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerManager playerManager)
        : base(playerManager)
    {
        Priority = 1;
    }
    public override void Enter()
    {
        // _playerManager.PlayerAnimationManager.PlayAttackAnimation();
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
