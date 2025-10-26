using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void HandlePlayerAnimations(PlayerLocomotionManager playerLocomotionManager, bool isLock)
    {
        if (isLock)
        {
            _animator.SetBool("IsLock", true);

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
        else
        {
            _animator.SetBool("IsLock", false);
            // Send intensity for free player movement.
            _animator.SetFloat("Intensity", playerLocomotionManager.GetIntensity, 0.1f, Time.deltaTime);

        }

        //Jump
        _animator.SetBool("IsGrounded", playerLocomotionManager.IsGrounded);
    }

}