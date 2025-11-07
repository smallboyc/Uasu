using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : CharacterAnimationManager
{
    public void PlayLocomotionAnimation(CharacterController characterController, PlayerLocomotionManager playerLocomotionManager, PlayerLockManager playerLockManager)
    {
        _animator.SetBool("IsLock", playerLockManager.IsLockedOnEnemy);

        //Lock state
        if (playerLockManager.IsLockedOnEnemy)
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
            return;
        }

        //Standard state
        _animator.SetFloat("Intensity", playerLocomotionManager.GetIntensity, 0.1f, Time.deltaTime);
        _animator.SetBool("IsGrounded", characterController.isGrounded);
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

}

