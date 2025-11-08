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
