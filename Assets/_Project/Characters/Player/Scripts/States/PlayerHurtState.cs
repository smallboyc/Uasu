
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    public PlayerHurtState(PlayerManager playerManager) : base(playerManager) { }

    public override void Enter()
    {
        // Debug.Log("PLAYER => Hurt State ENTER");
        _playerManager.Health--;
        _playerManager.AnimationManager.PlayHurtAnimation();
    }

    public override void Update()
    {
        if (!_playerManager.HurtManager.IsHurt)
        {
            // -> Walk
            if (_playerManager.LocomotionManager.IsMoving)
            {
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
            }
            else if (_playerManager.LockManager.IsLockedOnEnemy) // -> Lock
            {
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.LockState);
            }
            else // -> Idle
            {
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
            }

        }
    }

    public override void Exit()
    {
        _playerManager.AnimationManager.StopHurtAnimation();
        // Debug.Log("PLAYER => Hurt State EXIT");
    }
}
