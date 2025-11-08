
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

        if (_enemyManager.LockManager.HasLockedPlayer)
        {
            Debug.Log("ENEMY => Change to EnemyFocusState");
            //=> EnemyFocusState
        }

    }

    public override void Exit()
    {
        Debug.Log("ENEMY => Patrol EXIT");
        _enemyManager.AnimationManager.StopPatrolAnimation();
    }
}
