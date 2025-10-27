using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotionManager))]
[RequireComponent(typeof(EnemyLockManager))]
public class EnemyManager : CharacterManager
{
    [SerializeField] private List<Transform> _wayPoints;
    private EnemyLocomotionManager _enemyLocomotionManager;
    private EnemyLockManager _enemyLockManager;
    protected override void Awake()
    {
        base.Awake();
        _enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        _enemyLockManager = GetComponent<EnemyLockManager>();
    }

    protected override void Update()
    {
        base.Update();
        _enemyLockManager.TargetLockPlayer();
        _enemyLocomotionManager.HandleAllMovement(characterController, _wayPoints, _enemyLockManager);
    }

    public void SetWayPoints(List<Transform> enemySpawnerWaypoints)
    {
        _wayPoints = enemySpawnerWaypoints;
    }
}
