using UnityEngine;

public class SoulManager : MonoBehaviour
{
    [SerializeField] float _elevationLimit = 1.5f;
    [SerializeField] float _elevationSpeed = 0.5f;
    [SerializeField] float _scaleFactor = 0.2f;
    void Update()
    {
        if (transform.position.y < _elevationLimit)
        {
            transform.Translate(new Vector3(0.0f, _elevationSpeed * Time.deltaTime, 0.0f));
            transform.localScale += _scaleFactor * Time.deltaTime * Vector3.one;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.SoulCounter++;
            Destroy(gameObject);
        }
    }
}
