
using UnityEngine;

public class PlayerLockState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;

    public override void Enter()
    {
        // Debug.Log("Enter Lock State");
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(_playerManager.LockSounds, _playerManager.transform);
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
        if (PlayerInputManager.Instance.AttackPressed && _playerManager.AttackManager.CanAttack)
        {
            _playerManager.AttackManager.AttackCooldownCoroutine();
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.AttackState);
        }

        // Player has been hurt by enemy => Hurt
        if (_playerManager.HealthManager.IsHurt)
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
