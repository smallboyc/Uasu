
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        Debug.Log("ENEMY => Idle ENTER");
        _enemyManager.AnimationManager.PlayIdleAnimation();
    }

    public override void Update()
    {
        _enemyManager.LocomotionManager.HandleAllMovement(_enemyManager.CharacterController, _enemyManager.WayPoints, _enemyManager.LockManager);
        _enemyManager.LockManager.TargetLockPlayer();

        // Enemy Found Player => Focus
        if (_enemyManager.LockManager.HasLockedPlayer)
        {
            Debug.Log("ENEMY => Change to EnemyFocusState");
            //=> EnemyFocusState
        }
        // Enemy take a little break during their patrols => Idle State
        if (!_enemyManager.LocomotionManager.EnemyTakeABreak)
        {
            Debug.Log("ENEMY => Change to Patrol");
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.PatrolState);
        }
    }

    public override void Exit()
    {
        Debug.Log("ENEMY => Idle EXIT");
        _enemyManager.AnimationManager.StopIdleAnimation();
    }
}
