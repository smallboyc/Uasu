using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 1.5f;
    [SerializeField] private float _attackAngle = 50.0f;
    [SerializeField] private LayerMask _playerLayer;
    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;

    public void EnemyStartAttack()
    {
        // Debug.Log("Enemy Start Attack");
        _isAttacking = true;
    }

    public void EnemyEndAttack()
    {
        // Debug.Log("ANIM = Enemy End Attack");
        _isAttacking = false;
    }

    public void EnemyAttackHit()
    {
        // Debug.Log("ANIM = Enemy Attack Hit");
        Collider[] players = Physics.OverlapSphere(transform.position, _attackRadius, _playerLayer);
        foreach (Collider player in players)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            if (Vector3.Angle(transform.forward, dir) < _attackAngle)
            {
                // Debug.Log("ANIM = Enemy Touched Player");
                if (PlayerManager.Instance)
                {
                    PlayerManager.Instance.HurtManager.IsHurt = true; // => It will trigger the HurtState from the player current state.
                }
            }
            else
            {
                Debug.Log("Enemy Missed Player");
            }
        }
    }

    public bool IsPlayerInAttackRange()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, _attackRadius, _playerLayer);
        foreach (Collider player in players)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            return Vector3.Angle(transform.forward, dir) < _attackAngle;
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
#endif
}

