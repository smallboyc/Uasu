using UnityEngine;

public class EnemyAnimationManager : CharacterAnimationManager
{
    // Patrol
    public void PlayPatrolAnimation()
    {
        _animator.SetBool("IsPatrolling", true);
    }
    public void StopPatrolAnimation()
    {
        _animator.SetBool("IsPatrolling", false);
    }

    // Idle
    public void PlayIdleAnimation()
    {
        _animator.SetBool("IsPatrolling", false);
    }
    public void StopIdleAnimation()
    {
        _animator.SetBool("IsPatrolling", true);
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


    public void PlayLocomotionAnimation(bool enemyTakeABreak, bool hasLockedPlayer)
    {
        _animator.SetBool("IsTakingABreak", enemyTakeABreak);
        _animator.SetBool("HasLockedPlayer", hasLockedPlayer);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
