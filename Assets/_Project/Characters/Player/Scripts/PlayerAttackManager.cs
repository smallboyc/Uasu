using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private float _attackAngle = 50f;
    [SerializeField] private LayerMask _enemyLayer;

    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;


    // Only use these methods in the Attack Animation !!
    public void TriggerStartAttack()
    {
        Debug.Log("Trigger Start Attack");
        _isAttacking = true;
    }

    public void TriggerEndAttack()
    {
        Debug.Log("Trigger End Attack");
        _isAttacking = false;
    }

    // Appelé depuis un Animation Event ("AttackHitEvent")
    public void TriggerOnAttackHit()
    {
        Debug.Log("Trigger Hit Attack");
        Collider[] enemies = Physics.OverlapSphere(transform.position, _attackRadius, _enemyLayer);
        foreach (Collider enemy in enemies)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            dir.y = 0;

            if (Vector3.Angle(transform.forward, dir) < _attackAngle)
            {
                if (enemy.TryGetComponent(out EnemyHealthManager enemyHealth))
                {
                    enemyHealth.Hit(0.5f);

                    // if (enemy.TryGetComponent(out EnemyLocomotionManager enemyLocomotion))
                    // {
                    //     enemyLocomotion.StartCoroutine(
                    //         enemyLocomotion.SetKnockback(transform.forward, 3.0f)
                    //     );
                    // }
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
#endif
}