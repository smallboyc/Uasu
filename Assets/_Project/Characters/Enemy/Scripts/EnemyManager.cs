using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotionManager))]
[RequireComponent(typeof(EnemyLockManager))]
[RequireComponent(typeof(EnemyHealthManager))]
[RequireComponent(typeof(EnemyAttackManager))]
[RequireComponent(typeof(EnemyAnimationManager))]
public class EnemyManager : CharacterManager
{
    [SerializeField] private List<Transform> _wayPoints;
    private EnemyLocomotionManager _enemyLocomotionManager;
    private EnemyLockManager _enemyLockManager;
    private EnemyHealthManager _enemyHealthManager;
    private EnemyAttackManager _enemyAttackManager;
    private EnemyAnimationManager _enemyAnimationManager;
    protected override void Awake()
    {
        base.Awake();
        _enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        _enemyLockManager = GetComponent<EnemyLockManager>();
        _enemyAttackManager = GetComponent<EnemyAttackManager>();
        _enemyHealthManager = GetComponent<EnemyHealthManager>();
        _enemyAnimationManager = GetComponent<EnemyAnimationManager>();

        //Animations
        _enemyLocomotionManager.SetAnimationManager(_enemyAnimationManager);
        _enemyAttackManager.SetAnimationManager(_enemyAnimationManager);
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
            _enemyAttackManager.HandleAttack(_enemyLockManager);

            if (!_enemyAttackManager.IsAttacking)
            {
                _enemyLockManager.TargetLockPlayer();
                _enemyLocomotionManager.HandleAllMovement(CharacterController, _wayPoints, _enemyLockManager);
            }
        }
        else
        {
            _enemyLocomotionManager.HandleKnockback(CharacterController);
        }
    }

    public void SetWayPoints(List<Transform> enemySpawnerWaypoints)
    {
        _wayPoints = enemySpawnerWaypoints;
    }
}
