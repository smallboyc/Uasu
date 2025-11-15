using UnityEngine;

public class PlayerIdleState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;

    public override void Enter()
    {
        // Debug.Log("Idle");
        _playerManager.AnimationManager.PlayIdleAnimation();
    }

    public override void Update()
    {
        // -> We don't want to move during dialogue session.
        if (DialogueManager.Instance.DialogueIsRunning)
            return;
        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.LockManager.TargetLockEnemies();
        //

        // -> Move
        if (_playerManager.LocomotionManager.IsMoving)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
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
        if (_playerManager.HealthManager.IsHurt)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.HurtState);
        }

    }

    public override void Exit()
    {
        // Debug.Log("Idle State : EXIT");
    }


}
