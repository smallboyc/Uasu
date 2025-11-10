using System.Collections;
using UnityEngine;

public class CharacterLocomotionManager : MonoBehaviour
{
    //Knockback
    private Vector3 _knockbackDirection = Vector3.zero;
    private float _knockbackPower;
    protected virtual void Awake()
    {

    }

    public void HandleKnockback(CharacterController characterController)
    {
        if (_knockbackDirection != Vector3.zero)
        {
            characterController.Move(_knockbackDirection * _knockbackPower * Time.deltaTime);
            _knockbackPower -= Time.deltaTime;
        }
    }

    public IEnumerator SetKnockback(Vector3 direction, float power)
    {
        _knockbackDirection = direction;
        _knockbackPower = power;
        yield return new WaitForSeconds(0.5f);
        _knockbackDirection = Vector3.zero;
        _knockbackPower = 0.0f;
    }
}
