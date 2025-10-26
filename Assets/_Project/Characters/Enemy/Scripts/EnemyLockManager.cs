using UnityEngine;

public class EnemyLockManager : MonoBehaviour
{
    [SerializeField] private GameObject _targetPlayer;
    [SerializeField] private float _limitAngle = 80.0f;
    private string _playerTag = "Player";
    private bool _isLockedOnPlayer;

    public bool IsLockedOnPlayer
    {
        get
        {
            return _isLockedOnPlayer;
        }
    }

    public Transform GetPlayerTransform
    {
        get
        {
            if (_targetPlayer != null)
                return _targetPlayer.transform;
            return null;
        }
    }

    public void TargetLockPlayer()
    {
        if (_targetPlayer != null)
        {
            Vector3 direction = _targetPlayer.transform.position - transform.position;
            direction.y = 0.0f;
            direction.Normalize();

            float angle = Vector3.Angle(transform.forward, direction);
            if (angle < _limitAngle)
            {
                _isLockedOnPlayer = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _targetPlayer = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _targetPlayer = null;
            _isLockedOnPlayer = false;
        }
    }
}
