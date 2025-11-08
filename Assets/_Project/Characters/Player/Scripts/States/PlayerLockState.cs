
using UnityEngine;

public class PlayerLockState : PlayerState
{
    public PlayerLockState(PlayerManager playerManager) : base(playerManager) { }

    public override void Enter()
    {
        Debug.Log("Enter Lock State");
        _playerManager.AnimationManager.PlayLockAnimation();
    }

    public override void Update()
    {
        _playerManager.AnimationManager.UpdateLockAnimation(_playerManager.LocomotionManager);
        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.LockManager.TargetLockEnemies();
        //

        if (!_playerManager.LockManager.IsLockedOnEnemy)
        {
            if (_playerManager.LocomotionManager.IsMoving)
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
            else
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Lock State");
        _playerManager.AnimationManager.StopLockAnimation();
    }
}
