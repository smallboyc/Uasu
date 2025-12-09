using UnityEngine;

public class HealthBarrelManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _emptyBarrel;
    [SerializeField] private GameObject _filledBarrel;
    [SerializeField] private GameObject _healthItem;
    [Header("Health Data")]
    [SerializeField] private int _minHealth = 1;
    [SerializeField] private int _maxHealth = 2;
    //
    private GameObject _currentBarrel;
    private bool _playerInRange;
    private bool _used;


    void Start()
    {
        if (_filledBarrel)
            _currentBarrel = Instantiate(_filledBarrel, transform.position, Quaternion.Euler(90f, 0f, 0f));
    }

    void Update()
    {
        if (!_playerInRange)
            return;

        if (PlayerInputManager.Instance.InteractPressed && !_used)
        {
            _used = true;
            Destroy(_currentBarrel);
            if (_emptyBarrel)
                _currentBarrel = Instantiate(_emptyBarrel, transform.position, Quaternion.Euler(90f, 0f, 0f));
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
