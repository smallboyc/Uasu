using System;
using UnityEngine;

public class SorcererAnimationManager : CharacterAnimationManager
{
    // Sleep
    public void PlaySleepAnimation()
    {
        _animator.SetBool("IsSleeping", true);
    }
    public void StopSleepAnimation()
    {
        _animator.SetBool("IsSleeping", false);
    }

    // Idle
    public void PlayIdleAnimation()
    {

    }
    public void StopIdleAnimation()
    {

    }

    // WakeUp
    public void PlayWakeUpAnimation()
    {
        _animator.SetBool("IsWakingUp", true);
    }
    public void StopWakeUpAnimation()
    {
        _animator.SetBool("IsWakingUp", false);
    }
}
