using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _enemySpawnerWayPoints;
    [SerializeField] private GameObject _enemyPrefab;

    void Awake()
    {
        // Vérifications
        if (_enemyPrefab == null || _enemySpawnerWayPoints == null || _enemySpawnerWayPoints.Count == 0)
        {
            Debug.LogError($"[{name}] EnemySpawnerManager error: Please assign an enemy prefab and waypoints.");
            return;
        }

        if (_enemyPrefab.CompareTag("Enemy") == false)
        {
            Debug.LogError($"[{name}] EnemySpawnerManager error: The assigned prefab must have the 'Enemy' tag.");
            return;
        }

        GameObject enemyInstance = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);

        EnemyManager enemyManager = enemyInstance.GetComponent<EnemyManager>();
        if (enemyManager == null)
        {
            Debug.LogError($"[{name}] EnemySpawnerManager error: Enemy prefab has no EnemyManager component!");
            return;
        }

        enemyManager.SetWayPoints(transform.position, _enemySpawnerWayPoints);
    }
}
