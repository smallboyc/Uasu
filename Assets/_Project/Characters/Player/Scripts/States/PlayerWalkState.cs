
using UnityEngine;

public class PlayerWalkState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;

    public override void Enter()
    {
        // Debug.Log("Walk State : ENTER");
        _playerManager.AnimationManager.PlayWalkAnimation();
    }

    public override void Update()
    {
        // -> We don't want to move during dialogue session.
        if (DialogueManager.Instance.DialogueIsRunning)
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

        // Player enter
        if (_playerManager.CollisionManager.IsPushing)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.PushState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("Walk State : EXIT");
        _playerManager.AnimationManager.StopWalkAnimation();
    }
}
