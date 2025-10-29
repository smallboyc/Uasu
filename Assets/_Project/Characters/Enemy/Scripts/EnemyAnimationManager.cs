using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationManager : MonoBehaviour
{
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void HandleEnemyAnimations(CharacterController characterController, EnemyLocomotionManager enemyLocomotionManager)
    {
        _animator.SetBool("IsTakingABreak", enemyLocomotionManager.EnemyTakeABreak);
    }
}
