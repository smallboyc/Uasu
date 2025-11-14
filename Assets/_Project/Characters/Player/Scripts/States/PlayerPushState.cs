
using UnityEngine;

public class PlayerPushState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;

    public override void Enter()
    {
        _playerManager.AnimationManager.PlayPushAnimation();
    }

    public override void Update()
    {
        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.CollisionManager.CheckForObstacleCollision();
        //

        if (!_playerManager.CollisionManager.IsPushing)
        {
            if (_playerManager.LocomotionManager.IsMoving)
            {
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
            }
            else // -> Idle
            {
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
            }
        }
    }

    public override void Exit()
    {
        _playerManager.AnimationManager.StopPushAnimation();
    }
}
