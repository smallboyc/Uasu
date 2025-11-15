using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerLockManager : MonoBehaviour
{
    [Header("Lock Settings")]
    [SerializeField] private float _lockRadius = 8.0f;
    [SerializeField] private float _limitAngle = 60.0f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _lockCooldown = 0.25f;
    [SerializeField] private GameObject _potentialLockIndicatorPrefab;
    [SerializeField] private GameObject _lockIndicatorPrefab;

    private GameObject _bestEnemy = null;
    private GameObject _targetEnemy;
    private EnemyHealthManager _targetEnemyHealthManager;
    private GameObject _currentLockIndicator;
    private bool _isLockedOnEnemy;
    private bool _canToggleLock = true;
    private Vector3 _lockDirection;

    public bool IsLockedOnEnemy
    {
        get => _isLockedOnEnemy;
        set => _isLockedOnEnemy = value;
    }

    public Vector3 LockDirection => _lockDirection;

    // Main function used in the PlayerManager.
    public void TargetLockEnemies()
    {
        FindBestEnemyInRange();
        HandleLockToggle();
        UpdateLockState();
        SpawnLockIndicator();
        AutomaticUnlockSituation();
    }

    // Private functions
    private void FindBestEnemyInRange()
    {
        // We target the Enemy Character controller collider
        Collider[] enemies = Physics.OverlapSphere(transform.position, _lockRadius, _enemyLayer);
        float smallestAngle = Mathf.Infinity;
        _bestEnemy = null;

        foreach (Collider enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            directionToEnemy.y = 0f;
            float angle = Vector3.Angle(transform.forward, directionToEnemy);

            if (angle < _limitAngle && angle < smallestAngle)
            {
                smallestAngle = angle;
                _bestEnemy = enemy.gameObject;
            }
        }

    }

    private void AutomaticUnlockSituation()
    {
        //If Enemy is locked, but we exit our lock range => unlock the enemy
        if (_isLockedOnEnemy && _targetEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, _targetEnemy.transform.position);
            if (distance > _lockRadius)
            {
                UnlockEnemy();
            }
        }

        //If Enemy is dead
        if (_targetEnemyHealthManager && _targetEnemyHealthManager.IsDead())
        {
            UnlockEnemy();
            DestroyLockIndicator();
        }
    }

    private void HandleLockToggle()
    {
        if (_bestEnemy != null && PlayerInputManager.Instance.LockPressed && _canToggleLock)
        {
            _canToggleLock = false;
            StartCoroutine(LockCooldownRoutine());

            if (!_isLockedOnEnemy && _bestEnemy != null)
                LockEnemy(_bestEnemy);
            else
                UnlockEnemy();
        }

    }

    private void UpdateLockState()
    {
        if (_isLockedOnEnemy && _targetEnemy != null)
        {
            UpdateLockDirection();
            IsometricCameraManager.Instance.ActiveCameraLockEffect();
        }
        else
        {
            IsometricCameraManager.Instance.CancelCameraLockEffect();
        }
    }

    private void LockEnemy(GameObject enemy)
    {
        _targetEnemy = enemy;
        _targetEnemyHealthManager = _targetEnemy.GetComponent<EnemyHealthManager>();
        _isLockedOnEnemy = true;
    }

    private void UnlockEnemy()
    {
        if (_targetEnemy != null)
        {
            DestroyLockIndicator();
        }
        _targetEnemy = null;
        _isLockedOnEnemy = false;
    }

    private void UpdateLockDirection()
    {
        if (_targetEnemy == null) return;

        Vector3 directionToEnemy = _targetEnemy.transform.position - transform.position;
        directionToEnemy.y = 0f;

        _lockDirection = directionToEnemy.normalized;
    }

    private void SpawnLockIndicator()
    {
        DestroyLockIndicator();

        GameObject targetObject = null;
        GameObject prefabToUse = null;

        if (_targetEnemy != null)
        {
            targetObject = _targetEnemy;
            prefabToUse = _lockIndicatorPrefab;
        }
        else if (_bestEnemy != null)
        {
            targetObject = _bestEnemy;
            prefabToUse = _potentialLockIndicatorPrefab;
        }

        if (prefabToUse == null || targetObject == null)
            return;

        _currentLockIndicator = Instantiate(prefabToUse, targetObject.transform);
        _currentLockIndicator.transform.localPosition = new Vector3(0f, 3f, 0f);
        _currentLockIndicator.transform.localScale = Vector3.one * 0.5f;
    }

    private void DestroyLockIndicator()
    {
        if (_currentLockIndicator != null)
        {
            Destroy(_currentLockIndicator);
            _currentLockIndicator = null;
        }
    }

    private IEnumerator LockCooldownRoutine()
    {
        yield return new WaitForSeconds(_lockCooldown);
        _canToggleLock = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _isLockedOnEnemy ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _lockRadius);
    }
#endif
}
