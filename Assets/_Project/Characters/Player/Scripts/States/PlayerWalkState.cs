
using UnityEngine;

public class PlayerWalkState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;

    public override void Enter()
    {
        // Debug.Log("Walk State : ENTER");
        if (SoundManager.Instance)
            SoundManager.Instance.PlayLoopClip(_playerManager.WalkSounds, _playerManager.transform);
        _playerManager.AnimationManager.PlayWalkAnimation();
    }

    public override void Update()
    {
        // -> We don't want to move during dialogue session.
        if (DialogueManager.Instance && DialogueManager.Instance.DialogueIsRunning)
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);

        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.LockManager.TargetLockEnemies();
        _playerManager.CollisionManager.CheckForObstacleCollision();
        //

        // -> Idle
        if (!_playerManager.LocomotionManager.IsMoving)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }

        // -> Aerial
        if (!_playerManager.CharacterController.isGrounded)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.AerialState);
        }

        // -> Lock
        if (_playerManager.LockManager.IsLockedOnEnemy)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.LockState);
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

        // Player enter
        if (_playerManager.CollisionManager.IsPushing)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.PushState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("Walk State : EXIT");
        if (SoundManager.Instance)
            SoundManager.Instance.StopLoopSound();
        _playerManager.AnimationManager.StopWalkAnimation();
    }
}
