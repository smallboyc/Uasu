using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerLockManager : MonoBehaviour
{
    [SerializeField] private float _limitAngle = 60.0f; //must be > 0
    [SerializeField] private float _lockCooldown = 0.25f;
    private GameObject _targetEnemy;
    private List<GameObject> _enemiesInRange = new();
    private string _enemyTag = "Enemy";
    private bool _canToggleJoystick = true; //We use this variable to avoid joystick call on multiple frames.
    public Vector3 _lockDirection;
    private Color _basicEnemyColor = new(0.9622641f, 0.3495016f, 0.3495016f, 1f);
    private bool _isLockedOnEnemy;


    public bool IsLockedOnEnemy
    {
        get
        {
            return _isLockedOnEnemy;
        }
    }

    public Vector3 GetLockDirection
    {
        get
        {
            return _lockDirection;
        }
    }

    //Main function.
    public void TargetLockEnemies()
    {
        //If we're locking an enemy but we exit the range, then unlock.
        if (_enemiesInRange.Count == 0)
        {
            _targetEnemy = null;
        }

        GameObject bestEnemy = null;

        if (_targetEnemy == null)
        {
            _isLockedOnEnemy = false;
            IsometricCameraManager.Instance.CancelCameraLockEffect();
            TargetBestEnemy(ref bestEnemy);
            // HighlightBestEnemy(ref bestEnemy);
        }
        else
        {
            _isLockedOnEnemy = true;
            IsometricCameraManager.Instance.ActiveCameraLockEffect();
            // HighlightLockedEnemy();

        }

        if (PlayerInputManager.Instance._lockPressed && _canToggleJoystick)
        {
            ToggleLockEnemy(ref bestEnemy);
        }

        LockDirectionToEnemy();
    }



    // TRIGGER //
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_enemyTag))
        {
            _enemiesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_enemyTag))
        {
            // Renderer r = other.gameObject.GetComponent<Renderer>();
            // r.material.color = _basicEnemyColor;

            if (other.gameObject == _targetEnemy)
            {
                _targetEnemy = null;
            }

            _enemiesInRange.Remove(other.gameObject);
        }
    }
    // END OF TRIGGER //

    //Find the best enemy to target. (=> In range and with the lowest directional angle enemy/player)
    private void TargetBestEnemy(ref GameObject bestEnemy)
    {
        float smallestAngle = Mathf.Infinity;

        foreach (GameObject enemy in _enemiesInRange)
        {
            float angle = Vector3.Angle(transform.forward, enemy.transform.position - transform.position);

            if (angle < _limitAngle && angle < smallestAngle)
            {
                smallestAngle = angle;
                bestEnemy = enemy;
            }
        }
    }

    //Lock the best enemy
    private void ToggleLockEnemy(ref GameObject bestEnemy)
    {
        _canToggleJoystick = false;
        StartCoroutine(LockCooldown());
        if (_targetEnemy == null)
        {
            _targetEnemy = bestEnemy;
        }
        else
        {
            _targetEnemy = null;
        }
    }

    //Little trick to avoid joystick lock pression at multiple frames.
    private IEnumerator LockCooldown()
    {
        yield return new WaitForSeconds(_lockCooldown);
        _canToggleJoystick = true;
    }


    //A Yellow color applied to the best enemy found.
    // private void HighlightBestEnemy(ref GameObject bestEnemy)
    // {
    //     foreach (GameObject enemy in _enemiesInRange)
    //     {
    //         Renderer r = enemy.GetComponent<Renderer>();
    //         if (enemy == bestEnemy && enemy != _targetEnemy)
    //             r.material.color = Color.yellow;
    //         else
    //             r.material.color = _basicEnemyColor;
    //     }
    // }

    //A Blue color applied to the best enemy locked by the player.
    private void HighlightLockedEnemy()
    {
        if (_targetEnemy != null)
        {
            Renderer r = _targetEnemy.GetComponent<Renderer>();
            r.material.color = Color.blue;
        }
    }

    //Focus on the enemy by keeping the player eyes on the enemy locked with animtation.
    private void LockDirectionToEnemy()
    {
        if (_targetEnemy == null)
            return;

        Vector3 directionToEnemy = _targetEnemy.transform.position - transform.position;
        directionToEnemy.y = 0f;
        if (directionToEnemy.sqrMagnitude < 0.001f) return;

        _lockDirection = directionToEnemy.normalized;
    }
}
