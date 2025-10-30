using System.Collections;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 3.0f;
    [SerializeField] private float _limitAngle = 30.0f;
    [SerializeField] private LayerMask _enemyLayer;
    private bool _canAttack = true;
    private bool _isAttacking;
    private float _attackCooldown = 1.0f;
    private float _waitForHit = 0.3f;
    public bool IsAttacking => _isAttacking;

    public void HandleAttack()
    {
        if (PlayerInputManager.Instance.AttackPressed && _canAttack)
        {
            _isAttacking = true;
            _canAttack = false;
            StartCoroutine(AttackCooldown());

            Collider[] enemies = Physics.OverlapSphere(transform.position, _attackRadius, _enemyLayer);

            foreach (Collider enemy in enemies)
            {
                Vector3 directionToEnemy = enemy.transform.position - transform.position;
                directionToEnemy.y = 0f;
                float angle = Vector3.Angle(transform.forward, directionToEnemy);

                if (angle < _limitAngle)
                {
                    StartCoroutine(HitEnemy(enemy));
                }
            }
            StartCoroutine(AnimationCooldown());
        }


    }

    private IEnumerator AnimationCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        _isAttacking = false;
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private IEnumerator HitEnemy(Collider enemy)
    {
        yield return new WaitForSeconds(_waitForHit);
        EnemyHealthManager enemyHealthManager = enemy.gameObject.GetComponent<EnemyHealthManager>();
        enemyHealthManager.Hit();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
#endif
}
