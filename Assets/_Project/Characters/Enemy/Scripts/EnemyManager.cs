using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotionManager))]
public class EnemyManager : CharacterManager
{
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private List<Transform> _wayPoints;
    private EnemyLocomotionManager _enemyLocomotionManager;
    protected override void Awake()
    {
        base.Awake();
        _enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
    }

    protected override void Update()
    {
        base.Update();
        // _enemyLocomotionManager.HandleAllMovement();
    }

    public void SetWayPoints(Vector3 spawnPosition, List<Transform> enemySpawnerWaypoints)
    {
        _spawnPosition = spawnPosition;
        _wayPoints = enemySpawnerWaypoints;
    }
}
