using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationManager : MonoBehaviour
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void HandleEnemyAnimations(EnemyLocomotionManager enemyLocomotionManager, EnemyLockManager enemyLockManager, EnemyHealthManager enemyHealthManager)
    {
        _animator.SetBool("IsTakingABreak", enemyLocomotionManager.EnemyTakeABreak);
        _animator.SetBool("IsLockedOnPlayer", enemyLockManager.IsLockedOnPlayer);
        _animator.SetBool("IsStunned", enemyHealthManager.IsStunned);
    }
}
