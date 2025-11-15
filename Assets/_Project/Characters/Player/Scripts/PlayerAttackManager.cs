using System.Collections;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private float _attackAngle = 50f;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Sorcerer : (Attack = Wake up)")]
    [SerializeField] private float _wakeUpRadius = 1.5f;
    [SerializeField] private float _wakeUpAngle = 50f;
    [SerializeField] private LayerMask _sorcererMask;

    [Header("Attack")]
    [SerializeField] private float _attackCooldown = 1.0f;
    private bool _canAttack = true;
    [HideInInspector] public bool CanAttack => _canAttack;

    public void AttackCooldownCoroutine()
    {
        StartCoroutine(AttackCooldown());
    }
    public IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;

    // Start at Enter()
    public void TriggerStartAttack()
    {
        // Debug.Log("Trigger Start Attack");
        _isAttacking = true;
    }

    // Use as an Event in the Animation
    public void TriggerEndAttack()
    {
        // Debug.Log("Trigger End Attack");
        _isAttacking = false;
    }

    // Use as an Event in the Animation
    public void TriggerOnAttackHit()
    {
        TriggerAttackEnemy();
        TriggerWakeUpSorcerer();
    }


    private void TriggerAttackEnemy()
    {
        // ENEMY //
        // Debug.Log("Trigger Hit Attack");
        Collider[] enemies = Physics.OverlapSphere(transform.position, _attackRadius, _enemyLayer);
        foreach (Collider enemy in enemies)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            dir.y = 0;

            if (Vector3.Angle(transform.forward, dir) < _attackAngle)
            {
                EnemyHealthManager enemyHealthManager = enemy.gameObject.GetComponent<EnemyHealthManager>();
                EnemyAttackManager enemyAttackManager = enemy.gameObject.GetComponent<EnemyAttackManager>();
                if (enemyAttackManager && enemyAttackManager.IsAttacking)
                {
                    Debug.Log("CLASH!");
                    return;
                }
                if (enemyHealthManager)
                {
                    enemyHealthManager.Hurt(); // => It will trigger the HurtState from the enemy current state.
                }
            }
        }
    }

    private void TriggerWakeUpSorcerer()
    {
        // SORCERER // => Wake up the Sorcerer and been able to talk with him
        Collider[] sorcerers = Physics.OverlapSphere(transform.position, _wakeUpRadius, _sorcererMask);
        foreach (Collider sorcerer in sorcerers)
        {
            Vector3 dir = sorcerer.transform.position - transform.position;
            dir.y = 0;

            if (Vector3.Angle(transform.forward, dir) < _wakeUpAngle)
            {
                SorcererSleepManager sorcererSleepManager = sorcerer.gameObject.GetComponent<SorcererSleepManager>();
                if (sorcererSleepManager && sorcererSleepManager.IsSleeping)
                {
                    Debug.Log("WAKE UP !!!");
                    sorcererSleepManager.WakeUp();
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