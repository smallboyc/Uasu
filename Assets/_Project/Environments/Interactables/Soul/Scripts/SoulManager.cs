using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [SerializeField] float _elevationTime = 4.0f;
    [SerializeField] float _elevationSpeed = 0.5f;
    [SerializeField] float _rotationSpeed = 90f;

    //Sounds
    [Header("Sounds")]
    public AudioClip SoulCaught;

    private float _currentTime;

    void Start()
    {
        _currentTime = Time.time;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    void Update()
    {
        if (Time.time - _currentTime <= _elevationTime)
        {
            transform.Translate(0f, _elevationSpeed * Time.deltaTime, 0f, Space.World);
        }

        transform.Rotate(_rotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySoundClip(SoulCaught, transform);
            PlayerManager.Instance.SoulCounter++;
            Destroy(gameObject);
        }
    }
}
