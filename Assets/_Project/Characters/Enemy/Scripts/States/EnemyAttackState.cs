using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(_enemyManager.AttackSound, _enemyManager.transform);
        _enemyManager.AttackManager.EnemyStartAttack();
        _enemyManager.AnimationManager.PlayAttackAnimation();

    }

    public override void Update()
    {
        // Player is dead ? => Enemy chill
        if (PlayerManager.Instance.HealthManager.IsDead())
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.IdleState);

        if (!_enemyManager.AttackManager.IsAttacking)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.FocusState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("Stop attack anim");
        _enemyManager.AnimationManager.StopAttackAnimation();
    }
}
