
using UnityEngine;

public class PlayerAerialState : PlayerState
{
    public PlayerAerialState(PlayerManager playerManager)
        : base(playerManager)
    {
        Priority = 1;
    }
    public override void Enter()
    {
        Debug.Log("Aerial");
        _playerManager.AnimationManager.PlayAerialAnimation();
    }

    public override void Update()
    {
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController);
        if (_playerManager.CharacterController.isGrounded)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }
    }

    public override void Exit()
    {
        _playerManager.AnimationManager.StopAerialAnimation();
    }
}
