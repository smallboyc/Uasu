using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerManager playerManager)
        : base(playerManager)
    {
        Priority = 1;
    }

    public override void Enter()
    {
        Debug.Log("Attack State => ENTER");
        _playerManager.AttackManager.TriggerStartAttack();
        _playerManager.AnimationManager.PlayAttackAnimation();
        //Dès l'entrée faut lui mettre IsAttacking à true, car sinon la frame d'après, le temps que ça passe dans l'animation, ça revient à Idle
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
        Debug.Log("Attack State => EXIT");
        _playerManager.AnimationManager.StopAttackAnimation();
    }
}
