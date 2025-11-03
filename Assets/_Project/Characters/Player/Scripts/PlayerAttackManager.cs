using System.Collections;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 3.0f;
    [SerializeField] private float _limitAngle = 50.0f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _attackCooldown = 1.0f; //Time between two attacks
    [SerializeField] private float _waitForHit = 0.3f;
    private bool _isAttacking;
    private bool _isAttackCanceled;
    private bool _canAttack = true;
    public bool IsAttacking => _isAttacking;
    public bool IsAttackCanceled
    {
        get => _isAttackCanceled;
        set => _isAttackCanceled = value;
    }


    // Player can attack if the enemy is not attacking !
    public void HandleAttack(CharacterController characterController)
    {
        if (IsAttackCanceled)
            return;
            
        if (!_isAttacking && _canAttack && characterController.isGrounded && PlayerInputManager.Instance.AttackPressed)
        {
            StartCoroutine(HandleAttackFlow());

            // Detect if enemy is in range
            Collider[] enemies = Physics.OverlapSphere(transform.position, _attackRadius, _enemyLayer);
            foreach (Collider enemy in enemies)
            {
                Vector3 directionToEnemy = enemy.transform.position - transform.position;
                directionToEnemy.y = 0f;
                float angle = Vector3.Angle(transform.forward, directionToEnemy);
                if (angle < _limitAngle)
                {
                    StartCoroutine(HitEnemy(enemy));
                }
            }
        }
    }

    private IEnumerator HandleAttackFlow()
    {
        _isAttacking = true;
        _canAttack = false;

        // Wait the animation's end
        yield return new WaitForSeconds(0.7f);
        _isAttacking = false;

        // Wait a bit before a new attack
        yield return new WaitForSeconds(_attackCooldown - 0.7f);
        _canAttack = true;
    }

    private IEnumerator HitEnemy(Collider enemy)
    {
        yield return new WaitForSeconds(_waitForHit);
        EnemyHealthManager enemyHealthManager = enemy.gameObject.GetComponent<EnemyHealthManager>();
        if (enemyHealthManager != null)
        {
            enemyHealthManager.Hit(0.5f);
        }


        EnemyLockManager enemyLockManager = enemy.gameObject.GetComponent<EnemyLockManager>();
        //If player is not locked by enemy and permforms attack
        if (enemyLockManager != null && !enemyLockManager.IsLockedByPlayerPosition)
        {
            enemyLockManager.IsLockedByPlayerAttack = true;
        }

    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
#endif
}
