using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotionManager))]
[RequireComponent(typeof(EnemyLockManager))]
[RequireComponent(typeof(EnemyHealthManager))]
[RequireComponent(typeof(EnemyAnimationManager))]
public class EnemyManager : CharacterManager
{
    [SerializeField] private List<Transform> _wayPoints;
    private EnemyLocomotionManager _enemyLocomotionManager;
    private EnemyLockManager _enemyLockManager;
    private EnemyHealthManager _enemyHealthManager;
    private EnemyAnimationManager _enemyAnimationManager;
    protected override void Awake()
    {
        base.Awake();
        _enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        _enemyLockManager = GetComponent<EnemyLockManager>();
        _enemyAnimationManager = GetComponent<EnemyAnimationManager>();
        _enemyHealthManager = GetComponent<EnemyHealthManager>();
    }

    protected override void Update()
    {
        base.Update();
        if (_enemyHealthManager.Health <= 0)
        {
            _enemyHealthManager.GiveSoul();
            _enemyHealthManager.Die();
            return;
        }

        if (!_enemyHealthManager.IsStunned)
        {
            _enemyLockManager.TargetLockPlayer();
            _enemyLocomotionManager.HandleAllMovement(characterController, _wayPoints, _enemyLockManager);
        }

        _enemyAnimationManager.HandleEnemyAnimations(_enemyLocomotionManager, _enemyLockManager, _enemyHealthManager);

    }

    public void SetWayPoints(List<Transform> enemySpawnerWaypoints)
    {
        _wayPoints = enemySpawnerWaypoints;
    }
}
