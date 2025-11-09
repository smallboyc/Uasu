using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : CharacterAnimationManager
{
    //Idle
    public void PlayIdleAnimation()
    {
        StopWalkAnimation();
        StopAerialAnimation();
    }

    //Walk
    public void PlayWalkAnimation()
    {
        _animator.SetBool("IsMoving", true);
        _animator.SetBool("IsGrounded", true);
    }

    public void StopWalkAnimation()
    {
        _animator.SetBool("IsMoving", false);
    }

    // Jump
    public void PlayAerialAnimation()
    {
        _animator.SetBool("IsGrounded", false);
    }

    public void StopAerialAnimation()
    {
        _animator.SetBool("IsGrounded", true);
    }

    // Lock
    public void PlayLockAnimation()
    {
        _animator.SetBool("IsLock", true);

    }

    public void UpdateLockAnimation(PlayerLocomotionManager playerLocomotionManager)
    {
        Vector3 moveDirection = playerLocomotionManager.GetMoveDirection.normalized;

        // Send dot product btw moveDirection and both forward and right vectors.
        float forwardDot = Vector3.Dot(moveDirection, transform.forward);
        float rightDot = Vector3.Dot(moveDirection, transform.right);
        // Debug.Log("MD . F = " + forwardDot + " | MD . R = " + rightDot);

        // Snap
        forwardDot = forwardDot > 0.5f ? 1f : (forwardDot < -0.5f ? -1f : 0f);
        rightDot = rightDot > 0.5f ? 1f : (rightDot < -0.5f ? -1f : 0f);

        _animator.SetFloat("Forward", forwardDot, 0.1f, Time.deltaTime);
        _animator.SetFloat("Right", rightDot, 0.1f, Time.deltaTime);
    }

    public void StopLockAnimation()
    {
        _animator.SetBool("IsLock", false);
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

