using UnityEngine;

public class EnemyAnimationManager : CharacterAnimationManager
{
    // Idle
    public void PlayIdleAnimation()
    {
        StopPatrolAnimation();
        StopFocusAnimation();
    }
    
    // Patrol
    public void PlayPatrolAnimation()
    {
        _animator.SetBool("IsPatrolling", true);
    }
    public void StopPatrolAnimation()
    {
        _animator.SetBool("IsPatrolling", false);
    }

    // Focus
    public void PlayFocusAnimation()
    {
        _animator.SetBool("IsFocus", true);
    }
    public void StopFocusAnimation()
    {
        _animator.SetBool("IsFocus", false);
    }

    // Hurt
    public void PlayHurtAnimation()
    {
        _animator.SetBool("IsHurt", true);
    }
    public void StopHurtAnimation()
    {
        _animator.SetBool("IsHurt", false);
    }

    // Attack
    public void PlayAttackAnimation()
    {
        _animator.SetBool("IsAttacking", true);
    }

    public void StopAttackAnimation()
    {
        _animator.SetBool("IsAttacking", false);
    }
}
