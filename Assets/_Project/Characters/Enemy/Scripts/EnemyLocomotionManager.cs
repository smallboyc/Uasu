using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionManager : CharacterLocomotionManager
{
    private Vector3 _currentWayPointTarget = Vector3.zero;
    private int _currentWayPointIndex = -1;
    private float _walkSpeed = 1.2f;
    private float _runSpeed = 1.7f;
    [SerializeField] private float _moveSpeed;
    private float _walkRotationSpeed = 1.0f;
    private float _runRotationSpeed = 1.5f;
    [SerializeField] private float _rotationSpeed;
    private Vector3 _targetDirection;

    // Break
    [SerializeField] private float _breakSuccessProbability = 0.3f;
    [SerializeField] private float _breakCooldown = 4.0f;
    [SerializeField] private float _breakDuration = 3.0f;
    private bool _enemyTakeABreak;
    private bool _canLookForABreak = true;
    public bool EnemyTakeABreak => _enemyTakeABreak;

    public void HandleAllMovement(CharacterController characterController, List<Transform> wayPoints, EnemyLockManager enemyLockManager)
    {
        // Disable movement if enemy takes a break.
        if (!enemyLockManager.HasLockedPlayer && _enemyTakeABreak)
        {
            return;
        }

        // Enemy lock the player => No break permited, focus the player
        if (enemyLockManager.HasLockedPlayer)
        {
            _moveSpeed = _runSpeed;
            _rotationSpeed = _runRotationSpeed;
            _enemyTakeABreak = false;
            _targetDirection = enemyLockManager.Player.transform.position;
        }
        else // Player is not locked => Enemy make a round and can take a break.
        {
            _moveSpeed = _walkSpeed;
            _rotationSpeed = _walkRotationSpeed;
            CheckForWayPoints(wayPoints);
            _targetDirection = _currentWayPointTarget;

            if (_canLookForABreak)
            {
                _canLookForABreak = false;
                StartCoroutine(LookForABreak());
            }
        }
        EnableTargetMovement(characterController);
    }



    private IEnumerator LookForABreak()
    {
        yield return new WaitForSeconds(_breakCooldown);
        float randomFloat = Random.Range(0.0f, 1.0f);
        _enemyTakeABreak = _breakSuccessProbability > randomFloat;
        _canLookForABreak = true;

        if (_enemyTakeABreak)
        {
            yield return new WaitForSeconds(_breakDuration);
            _enemyTakeABreak = false;
        }
    }

    private void CheckForWayPoints(List<Transform> wayPoints)
    {
        if (wayPoints == null || wayPoints.Count == 0)
            return;

        if (_currentWayPointTarget == Vector3.zero ||
            Vector3.Distance(transform.position, _currentWayPointTarget) < 3.0f)
        {
            int randomIndex = Random.Range(0, wayPoints.Count);
            _currentWayPointIndex = randomIndex;
            _currentWayPointTarget = wayPoints[_currentWayPointIndex].position;
        }
    }

    private void EnableTargetMovement(CharacterController characterController)
    {
        Vector3 direction = _targetDirection - transform.position;
        direction.y = 0.0f;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        characterController.Move(transform.forward * _moveSpeed * Time.deltaTime);

        //Enemy is in the air, go back on the ground body
        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * 9.81f * Time.deltaTime);
        }
    }
}
