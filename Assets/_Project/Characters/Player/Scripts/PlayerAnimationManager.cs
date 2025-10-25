using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void HandlePlayerAnimations(PlayerLocomotionManager playerLocomotionManager)
    {
        _animator.SetFloat("Intensity", playerLocomotionManager.GetIntensity, 0.1f, Time.deltaTime);
        //Jump
        _animator.SetBool("IsGrounded", playerLocomotionManager.IsGrounded);
    }


}
