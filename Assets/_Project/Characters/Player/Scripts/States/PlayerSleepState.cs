
using UnityEngine;

public class PlayerSleepState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;
    public override void Enter()
    {
        _playerManager.AnimationManager.PlaySleepAnimation();
    }

    public override void Update()
    {
        if (PlayerInputManager.Instance.InteractPressed)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }
    }

    public override void Exit()
    {
        _playerManager.AnimationManager.StopSleepAnimation();
    }
}
