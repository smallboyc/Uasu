
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(PlayerManager playerManager)
        : base(playerManager)
    {
        Priority = 1;
    }


    public override void Enter()
    {
        Debug.Log("Walk");
        _playerManager.AnimationManager.PlayWalkAnimation();
    }

    public override void Update()
    {
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController);

        if (!_playerManager.LocomotionManager.IsMoving)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }

        if (!_playerManager.CharacterController.isGrounded)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.AerialState);
        }
    }

    public override void Exit()
    {
        _playerManager.AnimationManager.StopWalkAnimation();
    }
}
