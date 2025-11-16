using UnityEngine;

public class EnemyFocusState : EnemyState
{
    public EnemyFocusState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        // Debug.Log("ENEMY => Focus ENTER");
        _enemyManager.AnimationManager.PlayFocusAnimation();
    }

    public override void Update()
    {
        _enemyManager.LocomotionManager.HandleAllMovement(_enemyManager.CharacterController, _enemyManager.WayPoints, _enemyManager.LockManager);
        _enemyManager.LockManager.TargetLockPlayer();

        // Player is dead ? => Enemy chill
        if (PlayerManager.Instance.HealthManager.IsDead())
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.IdleState);

        // Enemy lost player focus => Go back to patrolling
        if (!_enemyManager.LockManager.HasLockedPlayer)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.PatrolState);
        }
        // Enemy has been hurt by player => Hurt
        if (_enemyManager.HealthManager.IsHurt)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.HurtState);
        }
        // Enemy attacks
        if (_enemyManager.AttackManager.IsPlayerInAttackRange() && _enemyManager.AttackManager.CanAttack)
        {
            _enemyManager.AttackManager.AttackCooldownCoroutine();
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.AttackState);
        }
    }

    public override void Exit()
    {
        // Debug.Log("ENEMY => Focus EXIT");
        _enemyManager.AnimationManager.StopFocusAnimation();
    }
}
