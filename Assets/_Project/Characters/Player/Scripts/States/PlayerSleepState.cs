
using UnityEngine;

public class PlayerSleepState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;
    public override void Enter()
    {
        IsometricCameraManager.Instance.ActiveCameraZoomEffect(2.0f);
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
        IsometricCameraManager.Instance.CancelCameraZoomEffect(2.5f);
    }
}
