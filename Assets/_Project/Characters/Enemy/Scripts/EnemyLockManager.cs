using UnityEngine;

public class EnemyLockManager : MonoBehaviour
{
    [SerializeField] private float _limitAngle = 80.0f;
    [SerializeField] private float _lockRadius = 5.0f;
    [SerializeField] private LayerMask _playerLayer;
    private GameObject _targetPlayer;
    private bool _hasLockedPlayer;
    private bool _isPlayerOnRange; //Range is the Trigger Sphere (so it doesn't mean the enemy saw the player)

    [HideInInspector] public bool HasLockedPlayer 
    {
        get => _hasLockedPlayer;
        set => _hasLockedPlayer = value;
    }

    public GameObject Player => _targetPlayer != null ? _targetPlayer : null;

    // Main function
    public void TargetLockPlayer()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, _lockRadius, _playerLayer);

        if (players.Length == 0)
        {
            ResetPlayerDetection();
            return;
        }
        GetSinglePlayer(players);
        CheckIfEnemySeeThePlayer();
        CheckIfEnemyIsTooFar();
    }


    // Private
    private void ResetPlayerDetection()
    {
        _isPlayerOnRange = false;
        _hasLockedPlayer = false;
        _targetPlayer = null;
    }

    private void CheckIfEnemySeeThePlayer()
    {
        Vector3 direction = _targetPlayer.transform.position - transform.position;
        direction.y = 0.0f;
        float angle = Vector3.Angle(transform.forward, direction);

        // Enemy detection
        if (angle < _limitAngle)
            _hasLockedPlayer = true;
    }

    private void CheckIfEnemyIsTooFar()
    {
        if (_targetPlayer != null && Vector3.Distance(transform.position, _targetPlayer.transform.position) > _lockRadius)
        {
            _hasLockedPlayer = false;
            _targetPlayer = null;
        }
    }

    private void GetSinglePlayer(Collider[] players)
    {
        _targetPlayer = players[0].gameObject;
        _isPlayerOnRange = true;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _hasLockedPlayer ? Color.red : (_isPlayerOnRange ? Color.yellow : Color.magenta);
        Gizmos.DrawWireSphere(transform.position, _lockRadius);
    }
#endif
}
