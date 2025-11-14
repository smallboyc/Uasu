using System;
using UnityEngine;

public class SorcererAnimationManager : CharacterAnimationManager
{
    // Sleep
    public void PlaySleepAnimation()
    {
        _animator.SetBool("IsSleeping", true);
    }

    // Wake Up
    public void PlayWakeUpAnimation()
    {
        _animator.SetBool("IsSleeping", false);
    }
}
