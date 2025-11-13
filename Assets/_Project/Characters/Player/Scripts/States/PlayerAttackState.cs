using UnityEngine;

public class PlayerAttackState : State
{
    private PlayerManager _playerManager = PlayerManager.Instance;

    public override void Enter()
    {
        // Debug.Log("Attack State => ENTER");
        _playerManager.AttackManager.TriggerStartAttack();
        _playerManager.AnimationManager.PlayAttackAnimation();
    }

    public override void Update()
    {
        if (!_playerManager.AttackManager.IsAttacking)
        {
            if (_playerManager.LocomotionManager.IsMoving)
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.WalkState);
            else
                _playerManager.PlayerStateMachine.ChangeState(_playerManager.IdleState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("Attack State => EXIT");
        _playerManager.AnimationManager.StopAttackAnimation();
    }
}
