using System.Collections;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private float _enemyAttackDistance = 2.0f;
    private float _limitAngle = 50.0f;
    private bool _canAttack = true;
    private bool _enemyAttackAnimation;
    private float _enemyAttackAnimationCooldown = 0.5f;
    private float _enemyAttackReloadCooldown = 2.0f;
    public bool IsAttacking => _enemyAttackAnimation;

    // Enemy attacks always have priority
    public void HandleAttack(EnemyLockManager enemyLockManager)
    {
        if (enemyLockManager.Player && _canAttack && PlayerOnEnemyAttackRange(enemyLockManager))
        {
            _enemyAttackAnimation = true;
            _canAttack = false;

            StartCoroutine(AttackCooldown(enemyLockManager));
        }
    }

    private bool PlayerOnEnemyAttackRange(EnemyLockManager enemyLockManager)
    {
        Vector3 directionToPlayer = enemyLockManager.Player.transform.position - transform.position;
        directionToPlayer.y = 0f;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        bool isPlayerOnRange = Vector3.Distance(transform.position, enemyLockManager.Player.transform.position) < _enemyAttackDistance;
        bool isEnemySawThePlayer = angle < _limitAngle;

        return isPlayerOnRange && isEnemySawThePlayer;
    }

    private IEnumerator AttackCooldown(EnemyLockManager enemyLockManager)
    {
        //Avoid Player attack during enemy's attack
        PlayerAttackManager playerAttackManager = enemyLockManager.Player.GetComponent<PlayerAttackManager>();
        playerAttackManager.IsAttackCanceled = true;

        //Animation
        yield return new WaitForSeconds(_enemyAttackAnimationCooldown);
        _enemyAttackAnimation = false;

        //Even if the attack is launch, we have to check if the player is still on the enemy's range to hit the player.
        if (PlayerOnEnemyAttackRange(enemyLockManager))
            Debug.Log("Hit!");
        else
            Debug.Log("Player dodge");

        playerAttackManager.IsAttackCanceled = false;
        
        //Attack reload
        yield return new WaitForSeconds(_enemyAttackReloadCooldown);
        _canAttack = true;
    }
}
