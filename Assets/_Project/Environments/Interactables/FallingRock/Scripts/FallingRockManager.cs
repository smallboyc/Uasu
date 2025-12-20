using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
class FallingRockSpawn
{
    public GameObject RockGameObject;
    private bool _isActive;

    public IEnumerator ResetActive()
    {
        _isActive = true;
        yield return new WaitForSeconds(5.0f);
        _isActive = false;
    }

    public bool IsActive() => _isActive;
}

public class FallingRockManager : MonoBehaviour
{
    [SerializeField] private GameObject _fallingRockPrefab;
    [SerializeField] private List<FallingRockSpawn> _fallingRockSpawns;
    [SerializeField] private bool _isFallingRockSessionActive;
    [SerializeField] private float _timeBetweenFalls = 2.0f;
    private bool _selectRockToFall = true;

    void Awake()
    {
        if (_fallingRockPrefab == null)
        {
            Debug.Log("No falling rock prefab founded.");
        }
    }

    void Update()
    {
        if (!PlayerManager.Instance)
            return;
            
        if (PlayerManager.Instance.HasAchievement("THE_SWORD"))
        {
            _isFallingRockSessionActive = true;
        }
        if (_isFallingRockSessionActive && _selectRockToFall)
        {
            int rand = Random.Range(0, _fallingRockSpawns.Count);
            FallingRockSpawn currentRockSpawn = _fallingRockSpawns[rand];

            if (currentRockSpawn.IsActive())
                return;

            Instantiate(_fallingRockPrefab, currentRockSpawn.RockGameObject.transform.position, Quaternion.identity);
            StartCoroutine(currentRockSpawn.ResetActive());
            StartCoroutine(NewFallingRock());
        }
    }

    private IEnumerator NewFallingRock()
    {
        _selectRockToFall = false;
        yield return new WaitForSeconds(_timeBetweenFalls);
        _selectRockToFall = true;
    }
}
