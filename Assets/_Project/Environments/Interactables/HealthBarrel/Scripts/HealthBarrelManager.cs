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

    //Sounds
    [Header("Sounds")]
    public AudioClip Interact;


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
            SoundManager.Instance.PlaySoundClip(Interact, transform);
            _used = true;
            Destroy(_currentBarrel);
            UIManager.Instance.Hide(PanelType.Help);
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
            if (!_used)
            {
                HelpManager.Instance.SetHelpText("<Interact>");
                UIManager.Instance.Show(PanelType.Help);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            UIManager.Instance.Hide(PanelType.Help);
        }
    }
}
