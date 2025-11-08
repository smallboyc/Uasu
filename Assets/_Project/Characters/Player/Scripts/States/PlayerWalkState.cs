
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
        Debug.Log("Walk State : ENTER");
        _playerManager.AnimationManager.PlayWalkAnimation();
    }

    public override void Update()
    {
        _playerManager.LocomotionManager.HandleAllMovement(_playerManager.CharacterController);

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

        // -> Attack
        // Vérifie l'attaque uniquement si aucune coroutine de cooldown n'est en cours
        if (PlayerInputManager.Instance.AttackPressed && _playerManager.CanAttack)
        {
            _playerManager.PlayerStateMachine.ChangeState(_playerManager.AttackState);
            _playerManager.StartCoroutine(_playerManager.AttackCooldown());
        }
    }

    public override void Exit()
    {
        Debug.Log("Walk State : EXIT");
        _playerManager.AnimationManager.StopWalkAnimation();
    }
}
