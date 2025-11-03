using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationManager : MonoBehaviour
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void HandleEnemyAnimations(EnemyLocomotionManager enemyLocomotionManager, EnemyLockManager enemyLockManager, EnemyHealthManager enemyHealthManager, EnemyAttackManager enemyAttackManager)
    {
        _animator.SetBool("IsTakingABreak", enemyLocomotionManager.EnemyTakeABreak);
        _animator.SetBool("IsAttacking", enemyAttackManager.IsAttacking);
        _animator.SetBool("HasLockedPlayer", enemyLockManager.HasLockedPlayer);
        _animator.SetBool("IsStunned", enemyHealthManager.IsStunned);
    }
}
