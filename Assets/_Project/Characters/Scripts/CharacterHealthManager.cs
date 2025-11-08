using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimationManager))]
public class CharacterHealthManager : MonoBehaviour
{
    [SerializeField] protected int _health;
    private bool _isStunned;
    public int Health => _health;
    public bool IsStunned => _isStunned;

    // CharacterAnimationManager characterAnimationManager;

    private void Awake()
    {
        // characterAnimationManager = GetComponent<CharacterAnimationManager>();
    }
    public virtual void Hit(float stunnedCooldown = 0.2f)
    {
        // _isStunned = true;
        // characterAnimationManager.PlayHitAnimation();
        // Debug.Log($"{name} has been hit!");
        // StartCoroutine(StunnedCooldown(stunnedCooldown));
    }

    public virtual void Feal()
    {

    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} die.");
        Destroy(gameObject);
    }

    private IEnumerator StunnedCooldown(float stunnedCooldown)
    {
        yield return new WaitForSeconds(stunnedCooldown);
        _isStunned = false;

    }
}
