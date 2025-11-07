using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimationManager : MonoBehaviour
{
    protected Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void PlayHitAnimation()
    {
        _animator.SetTrigger("Hit");
    }
}
