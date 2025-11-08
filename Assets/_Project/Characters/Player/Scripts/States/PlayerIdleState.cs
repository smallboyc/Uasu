
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerManager playerManager)
        : base(playerManager)
    {
        Priority = 1;
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        _playerManager.AnimationManager.PlayIdleAnimation();
    }


    public override void Update()
    {
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController);

        if (_playerManager.LocomotionManager.IsMoving)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
        }

        if (!_playerManager.CharacterController.isGrounded)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.AerialState);
        }
    }

    public override void Exit()
    {
        // clean
    }
}

