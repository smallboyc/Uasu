
using UnityEngine;

public class PlayerLockState : PlayerState
{
    public PlayerLockState(PlayerManager playerManager) : base(playerManager) { }

    public override void Enter()
    {
        // Debug.Log("Enter Lock State");
        _playerManager.AnimationManager.PlayLockAnimation();
    }

    public override void Update()
    {
        _playerManager.AnimationManager.UpdateLockAnimation(_playerManager.LocomotionManager);
        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.LockManager.TargetLockEnemies();
        //

        // -> Unlock
        if (!_playerManager.LockManager.IsLockedOnEnemy)
        {
            // -> Walk
            if (_playerManager.LocomotionManager.IsMoving)
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
            else // -> Idle
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }

        // -> Attack
        if (PlayerInputManager.Instance.AttackPressed && _playerManager.CanAttack)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.AttackState);
            _playerManager.StartCoroutine(_playerManager.AttackCooldown());
        }

        // Player has been hurt by enemy => Hurt
        if (_playerManager.HurtManager.IsHurt)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.HurtState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("Exit Lock State");
        _playerManager.AnimationManager.StopLockAnimation();
    }
}
