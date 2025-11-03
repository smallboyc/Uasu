using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 3.0f;
    [SerializeField] private float _limitAngle = 50.0f;
    [SerializeField] private float _waitForHit = 0.3f;
    [SerializeField] private LayerMask _enemyLayer;
    private float _animationCooldown = 0.7f;
    private float _attackReloadCooldown = 1.0f; //Time between two attacks
    private bool _isAttacking;
    private bool _canAttack = true;
    public bool IsAttacking => _isAttacking;

    // Player can attack if the enemy is not attacking !
    public void HandleAttack(CharacterController characterController)
    {
        if (!_isAttacking && _canAttack && characterController.isGrounded && PlayerInputManager.Instance.AttackPressed)
        {
            StartCoroutine(HandleAttackFlow());

            //1. Detect if enemy is in range
            Collider[] enemies = Physics.OverlapSphere(transform.position, _attackRadius, _enemyLayer);
            foreach (Collider enemy in enemies)
            {
                //2. Check if enemy is in the player vision
                Vector3 directionToEnemy = enemy.gameObject.transform.position - transform.position;
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
        yield return new WaitForSeconds(_animationCooldown);
        _isAttacking = false;

        // Wait a bit before a new attack
        yield return new WaitForSeconds(_attackReloadCooldown - _animationCooldown);
        _canAttack = true;
    }

    private IEnumerator HitEnemy(Collider enemy)
    {
        yield return new WaitForSeconds(_waitForHit);

        EnemyHealthManager enemyHealthManager = enemy.gameObject.GetComponent<EnemyHealthManager>();
        if (enemyHealthManager != null)
        {
            enemyHealthManager.Hit(0.5f);
            //Set a knockback to the enemy
            EnemyLocomotionManager enemyLocomotionManager = enemy.gameObject.GetComponent<EnemyLocomotionManager>();
            StartCoroutine(enemyLocomotionManager.SetKnockback(transform.forward, 3.0f));

            //Player surprises the enemy with an attack => Enemy is angry!
            EnemyLockManager enemyLockManager = enemy.gameObject.GetComponent<EnemyLockManager>();
            if (!enemyLockManager.HasLockedPlayer)
            {
                enemyLockManager.HasLockedPlayer = true;
            }
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
