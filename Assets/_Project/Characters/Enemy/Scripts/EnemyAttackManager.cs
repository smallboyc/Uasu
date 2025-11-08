using System.Collections;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 2.0f;
    [SerializeField] private float _limitAngle = 50.0f;
    [SerializeField] private float _hitDelay = 0.7f;
    [SerializeField] private float _attackReloadCooldown = 1.0f;

    private bool _isAttacking;
    public bool IsAttacking => _isAttacking;

    public void HandleAttack(EnemyLockManager enemyLockManager)
    {
        if (enemyLockManager.Player && !_isAttacking && PlayerOnEnemyAttackRange(enemyLockManager))
        {
            StartCoroutine(HandleAttackFlow());

            StartCoroutine(HandleHit(enemyLockManager));
        }
    }

    private GameObject PlayerOnEnemyAttackRange(EnemyLockManager enemyLockManager)
    {
        Vector3 directionToPlayer = enemyLockManager.Player.transform.position - transform.position;
        directionToPlayer.y = 0f;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        bool isPlayerOnRange = Vector3.Distance(transform.position, enemyLockManager.Player.transform.position) < _attackRadius;
        bool isEnemySawThePlayer = angle < _limitAngle;

        if (isPlayerOnRange && isEnemySawThePlayer)
            return enemyLockManager.Player;

        return null;
    }

    private IEnumerator HandleAttackFlow()
    {
        _isAttacking = true;
        // Wait a bit before a new attack
        yield return new WaitForSeconds(_attackReloadCooldown);
        _isAttacking = false;
    }

    private IEnumerator HandleHit(EnemyLockManager enemyLockManager)
    {
        yield return new WaitForSeconds(_hitDelay);

        GameObject player = PlayerOnEnemyAttackRange(enemyLockManager);
        if (player)
        {
            PlayerHealthManager playerHealthManager = player.GetComponent<PlayerHealthManager>();
            playerHealthManager.Hit();
            PlayerLocomotionManager playerLocomotionManager = player.GetComponent<PlayerLocomotionManager>();
            StartCoroutine(playerLocomotionManager.SetKnockback(transform.forward, 8.0f));
        }
        else
            Debug.Log("Player dodge");
    }
}
