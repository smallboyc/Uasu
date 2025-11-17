
using UnityEngine;

public class PlayerDeathState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;

    public override void Enter()
    {
        _playerManager.AnimationManager.PlayDeathAnimation();
        UIManager.Instance.GameOver();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
    }
}
