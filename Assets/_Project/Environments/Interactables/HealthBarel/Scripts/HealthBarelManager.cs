using UnityEngine;

public class HealthBarelManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _emptyBarel;
    [SerializeField] private GameObject _filledBarel;
    [SerializeField] private GameObject _healthItem;
    [Header("Health Data")]
    [SerializeField] private int _minHealth = 1;
    [SerializeField] private int _maxHealth = 2;
    //
    private GameObject _currentBarel;
    private bool _playerInRange;
    private bool _used;


    void Start()
    {
        if (_filledBarel)
            _currentBarel = Instantiate(_filledBarel, transform.position, Quaternion.Euler(90f, 0f, 0f));
    }

    void Update()
    {
        if (!_playerInRange)
            return;

        if (PlayerInputManager.Instance.InteractPressed && !_used)
        {
            _used = true;
            Destroy(_currentBarel);
            if (_emptyBarel)
                _currentBarel = Instantiate(_emptyBarel, transform.position, Quaternion.Euler(90f, 0f, 0f));
            InstantiateHealth();
        }
    }

    private void InstantiateHealth()
    {
        int health = Random.Range(_minHealth, _maxHealth + 1);
        Debug.Log(health);
        for (int i = 0; i < health; i++)
        {
            Instantiate(_healthItem, transform.position, Quaternion.identity);
        }
    }


    // Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}
