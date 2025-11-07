using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private float _limitAngle = 50.0f;
    [SerializeField] private float _hitDelay = 0.3f;
    [SerializeField] private float _attackReloadCooldown = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;
    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;
    private PlayerAnimationManager _playerAnimationManager;
    public void SetAnimationManager(PlayerAnimationManager playerAnimationManager) => _playerAnimationManager = playerAnimationManager;

    public void HandleAttack(CharacterController characterController)
    {
        if (!_isAttacking && characterController.isGrounded && PlayerInputManager.Instance.AttackPressed)
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
                    StartCoroutine(HandleHit(enemy));
                }
            }
        }
    }

    private IEnumerator HandleAttackFlow()
    {
        _isAttacking = true;

        // Avoid looping animation attack (just one time when the character is attacking)
        _playerAnimationManager.PlayAttackAnimation();

        // Wait a bit before a new attack
        yield return new WaitForSeconds(_attackReloadCooldown);
        _isAttacking = false;
    }

    private IEnumerator HandleHit(Collider enemy)
    {
        yield return new WaitForSeconds(_hitDelay);

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
