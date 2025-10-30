using System.Collections;
using UnityEngine;

public class CharacterHealthManager : MonoBehaviour
{
    [SerializeField] protected int _health;
    private bool _isStunned;
    public int Health => _health;
    public bool IsStunned => _isStunned;
    public virtual void Hit(float stunnedCooldown = 0.2f)
    {
        _isStunned = true;
        StartCoroutine(StunnedCooldown(stunnedCooldown));
    }

    public virtual void Feal()
    {

    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} die.");
        Destroy(gameObject);
    }

    private IEnumerator StunnedCooldown(float stunnedCooldo)
    {
        yield return new WaitForSeconds(stunnedCooldo);
        _isStunned = false;
    }
}
