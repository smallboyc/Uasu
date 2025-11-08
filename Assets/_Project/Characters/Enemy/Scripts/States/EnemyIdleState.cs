
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
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.FocusState);
        }
        // Enemy wake up from his break => Patrol
        if (!_enemyManager.LocomotionManager.EnemyTakeABreak)
        {
            _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.PatrolState);
        }
    }

    public override void Exit()
    {
        Debug.Log("ENEMY => Idle EXIT");
        _enemyManager.AnimationManager.StopIdleAnimation();
    }
}
