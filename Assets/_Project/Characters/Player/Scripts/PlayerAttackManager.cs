using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private float _attackAngle = 50f;
    [SerializeField] private LayerMask _enemyLayer;

    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;

    // Start at Enter()
    public void TriggerStartAttack()
    {
        Debug.Log("Trigger Start Attack");
        _isAttacking = true;
    }

    // Use as an Event in the Animation
    public void TriggerEndAttack()
    {
        Debug.Log("Trigger End Attack");
        _isAttacking = false;
    }

    // Use as an Event in the Animation
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
                EnemyHurtManager enemyHurtManager = enemy.gameObject.GetComponent<EnemyHurtManager>();
                if (enemyHurtManager)
                {
                    enemyHurtManager.IsHurt = true; // => It will trigger the HurtState from the enemy current state.
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