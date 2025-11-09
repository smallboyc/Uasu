
using UnityEngine;

public class PlayerAerialState : PlayerState
{
    public PlayerAerialState(PlayerManager playerManager) : base(playerManager) { }
    public override void Enter()
    {
        // Debug.Log("Aerial State => ENTER");
        _playerManager.AnimationManager.PlayAerialAnimation();
    }

    public override void Update()
    {
        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.LockManager.TargetLockEnemies();
        //

        if (_playerManager.CharacterController.isGrounded)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("Aerial State => EXIT");
        _playerManager.AnimationManager.StopAerialAnimation();
    }
}
