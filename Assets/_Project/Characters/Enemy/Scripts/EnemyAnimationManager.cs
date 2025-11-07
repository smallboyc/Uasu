using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationManager : CharacterAnimationManager
{
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
