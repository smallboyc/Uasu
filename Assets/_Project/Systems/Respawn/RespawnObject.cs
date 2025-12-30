using UnityEngine;

public class RespawnObject : MonoBehaviour
{
    private Vector3 _originPosition;
    void Start()
    {
        _originPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadzone"))
        {
            transform.position = _originPosition;
        }
    }
}
