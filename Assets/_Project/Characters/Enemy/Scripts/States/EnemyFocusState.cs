using UnityEngine;

public class EnemyFocusState : EnemyState
{
    public EnemyFocusState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        Debug.Log("ENEMY => Focus ENTER");
        _enemyManager.AnimationManager.PlayFocusAnimation();
    }

    public override void Update()
    {
        _enemyManager.LocomotionManager.HandleAllMovement(_enemyManager.CharacterController, _enemyManager.WayPoints, _enemyManager.LockManager);
        _enemyManager.LockManager.TargetLockPlayer();

        // Enemy lost player focus => Go back to patrolling
        if (!_enemyManager.LockManager.HasLockedPlayer)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.PatrolState);
        }
        // Enemy has been hurt by player => Hurt
        if (_enemyManager.HurtManager.IsHurt)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.HurtState);
        }
        // Enemy attacks
        if (_enemyManager.AttackManager.IsPlayerInAttackRange() && _enemyManager.CanAttack)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.AttackState);
            _enemyManager.StartCoroutine(_enemyManager.AttackCooldown());
        }
    }

    public override void Exit()
    {
        // Debug.Log("ENEMY => Focus EXIT");
        _enemyManager.AnimationManager.StopFocusAnimation();
    }
}
