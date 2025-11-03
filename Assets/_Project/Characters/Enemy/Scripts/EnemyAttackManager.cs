using System.Collections;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private float _enemyAttackDistance = 2.0f;
    private float _limitAngle = 50.0f;
    private bool _enemyAttackAnimation;
    private float _AnimationCooldown = 0.5f;
    private float _AttackReloadCooldown = 1.0f;
    private bool _canAttack = true;
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

    private GameObject PlayerOnEnemyAttackRange(EnemyLockManager enemyLockManager)
    {
        Vector3 directionToPlayer = enemyLockManager.Player.transform.position - transform.position;
        directionToPlayer.y = 0f;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        bool isPlayerOnRange = Vector3.Distance(transform.position, enemyLockManager.Player.transform.position) < _enemyAttackDistance;
        bool isEnemySawThePlayer = angle < _limitAngle;

        if (isPlayerOnRange && isEnemySawThePlayer)
            return enemyLockManager.Player;

        return null;
    }

    private IEnumerator AttackCooldown(EnemyLockManager enemyLockManager)
    {
        //Animation
        yield return new WaitForSeconds(_AnimationCooldown);
        _enemyAttackAnimation = false;

        //Even if the attack is launch, we have to check if the player is still on the enemy's range to hit the player.
        GameObject player = PlayerOnEnemyAttackRange(enemyLockManager);
        if (player)
        {
            PlayerHealthManager playerHealthManager = player.GetComponent<PlayerHealthManager>();
            playerHealthManager.Hit();
        }
        else
            Debug.Log("Player dodge");

        //Attack reload
        yield return new WaitForSeconds(_AttackReloadCooldown);
        _canAttack = true;
    }
}
