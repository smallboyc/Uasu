using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerManager playerManager) : base(playerManager) { }

    public override void Enter()
    {
        Debug.Log("Idle");
        _playerManager.AnimationManager.PlayIdleAnimation();
    }

    public override void Update()
    {
        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.LockManager.TargetLockEnemies();
        //
        
        // -> Idle
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
    }

    public override void Exit()
    {
        Debug.Log("Idle State : EXIT");
    }


}
