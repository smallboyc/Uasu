
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        Debug.Log("ENEMY => Patrol ENTER");
        _enemyManager.AnimationManager.PlayPatrolAnimation();
    }

    public override void Update()
    {
        _enemyManager.LocomotionManager.HandleAllMovement(_enemyManager.CharacterController, _enemyManager.WayPoints, _enemyManager.LockManager);
        _enemyManager.LockManager.TargetLockPlayer();

        // Enemy Found Player => Focus
        if (_enemyManager.LockManager.HasLockedPlayer)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.FocusState);
        }
        // Enemy take a little break during their patrols => Idle State
        if (_enemyManager.LocomotionManager.EnemyTakeABreak)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.IdleState);
        }

    }

    public override void Exit()
    {
        Debug.Log("ENEMY => Patrol EXIT");
        _enemyManager.AnimationManager.StopPatrolAnimation();
    }
}
