
using UnityEngine;

public class PlayerAerialState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;
    public override void Enter()
    {
        // Debug.Log("Aerial State => ENTER");        
        _playerManager.AnimationManager.PlayAerialAnimation();
        SoundManager.Instance.PlaySoundClip(_playerManager.JumpSounds, _playerManager.transform);
    }

    public override void Update()
    {
        //
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController, _playerManager.LockManager);
        _playerManager.LockManager.TargetLockEnemies();
        //

        // -> We don't want to move during dialogue session.
        if (DialogueManager.Instance.DialogueIsRunning)
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);

        // -> Falling and touching the ground
        if (_playerManager.CharacterController.isGrounded)
        {
            if (_playerManager.LocomotionManager.IsMoving)
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
            else
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("Aerial State => EXIT");
        _playerManager.AnimationManager.StopAerialAnimation();
    }
}
