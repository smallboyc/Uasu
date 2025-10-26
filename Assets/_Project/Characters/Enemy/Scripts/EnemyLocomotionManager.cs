using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionManager : CharacterLocomotionManager
{
    Vector3 _currentWayPointTarget = Vector3.zero;
    private int _currentWayPointIndex = -1;
    private float _moveSpeed = 2.0f;
    private float _rotationSpeed = 2.0f;

    Vector3 _targetDirection;

    public void HandleAllMovement(CharacterController characterController, List<Transform> wayPoints, EnemyLockManager enemyLockManager)
    {
        enemyLockManager.TargetLockPlayer();

        if (!enemyLockManager.IsLockedOnPlayer)
        {
            CheckForWayPoints(wayPoints);
            _targetDirection = _currentWayPointTarget;
        }
        else
        {
            _targetDirection = enemyLockManager.GetPlayerTransform.position;
        }

        EnableTargetMovement(characterController);

    }

    private void CheckForWayPoints(List<Transform> wayPoints)
    {
        if (wayPoints == null || wayPoints.Count == 0)
            return;

        if (_currentWayPointTarget == Vector3.zero ||
            Vector3.Distance(transform.position, _currentWayPointTarget) < 1.0f)
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

        if (!characterController.isGrounded)
        {
            characterController.Move(Vector3.down * 9.81f * Time.deltaTime);
        }
    }
}



