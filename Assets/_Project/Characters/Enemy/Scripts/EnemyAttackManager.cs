// using System.Collections;
// using UnityEngine;

// public class EnemyAttackManager : MonoBehaviour
// {
//     [SerializeField] private float _attackRadius = 2.0f;
//     [SerializeField] private float _attackAngle = 50.0f;

//     private bool _isAttacking = false;
//     public bool IsAttacking => _isAttacking;

//     public void EnemyStartAttack()
//     {
//         Debug.Log("Enemy Start Attack");
//         _isAttacking = true;
//     }

//     // Use as an Event in the Animation
//     public void EnemyEndAttack()
//     {
//         Debug.Log("Enemy End Attack");
//         _isAttacking = false;
//     }

//     public void EnemyAttackHit(EnemyLockManager enemyLockManager)
//     {
//         if (PlayerOnEnemyAttackRange(enemyLockManager))
//         {
//             Debug.Log("Enemy Touched Player");
//         }
//         else
//         {
//             Debug.Log("Enemy Missed Player");
//         }
//     }


//     // Useful to trigger the Attack state but also to check during hit if the player is steel on range.
//     public GameObject PlayerOnEnemyAttackRange(EnemyLockManager enemyLockManager)
//     {
//         if (!enemyLockManager.Player)
//             return null;

//         Vector3 directionToPlayer = enemyLockManager.Player.transform.position - transform.position;
//         directionToPlayer.y = 0f;
//         float angle = Vector3.Angle(transform.forward, directionToPlayer);

//         bool isPlayerOnRange = Vector3.Distance(transform.position, enemyLockManager.Player.transform.position) < _attackRadius;
//         bool isEnemySawThePlayer = angle < _attackAngle;

//         if (isPlayerOnRange && isEnemySawThePlayer)
//             return enemyLockManager.Player;

//         return null;
//     }
// }


using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    [SerializeField] private float _attackRadius = 2.0f;
    [SerializeField] private float _attackAngle = 50.0f;
    [SerializeField] private LayerMask _playerLayer;
    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;

    public void EnemyStartAttack()
    {
        Debug.Log("Enemy Start Attack");
        _isAttacking = true;
    }

    public void EnemyEndAttack()
    {
        Debug.Log("ANIM = Enemy End Attack");
        _isAttacking = false;
    }

    public void EnemyAttackHit()
    {
        Debug.Log("ANIM = Enemy Attack Hit");
        Collider[] players = Physics.OverlapSphere(transform.position, _attackRadius, _playerLayer);
        foreach (Collider player in players)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.y = 0;
            if (Vector3.Angle(transform.forward, dir) < _attackAngle)
            {
                Debug.Log("ANIM = Enemy Touched Player");
                PlayerHurtManager playerHurtManager = player.gameObject.GetComponent<PlayerHurtManager>();
                if (playerHurtManager)
                {
                    playerHurtManager.IsHurt = true; // => It will trigger the HurtState from the player current state.
                }
            }
            else
            {
                Debug.Log("Enemy Missed Player");
            }
        }
    }

    // Méthode utile pour vérifier si le joueur est dans la zone d'attaque (optionnel)
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

