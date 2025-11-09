using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        _enemyManager.AttackManager.EnemyStartAttack();
        _enemyManager.AnimationManager.PlayAttackAnimation();

    }

    public override void Update()
    {
        if (!_enemyManager.AttackManager.IsAttacking)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.FocusState);
        }
    }

    public override void Exit()
    {
        Debug.Log("Stop attack anim");
        _enemyManager.AnimationManager.StopAttackAnimation();
    }
}
