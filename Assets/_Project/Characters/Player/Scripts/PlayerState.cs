public abstract class PlayerState : State
{
    protected PlayerManager _playerManager;
    protected PlayerAnimationManager _animationManager;

    public PlayerState(PlayerManager playerManager, PlayerAnimationManager animationManager)
    {
        _playerManager = playerManager;
        _animationManager = animationManager;
    }
}