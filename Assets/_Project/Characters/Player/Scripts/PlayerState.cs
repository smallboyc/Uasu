public abstract class PlayerState : State
{
    protected PlayerManager _playerManager;
   
    public PlayerState(PlayerManager playerManager)
    {
        _playerManager = playerManager;
    }
}