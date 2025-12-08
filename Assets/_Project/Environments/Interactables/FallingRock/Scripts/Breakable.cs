using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject _intactObject;
    [SerializeField] private GameObject _breakObject;
    [SerializeField] private GameObject _target;
    private GameObject _targetInstance;
    private float _offset = 0.95f;
    [SerializeField] private LayerMask _layer;
    void Awake()
    {
        if (_intactObject)
        {
            _intactObject.SetActive(true);
            _target.SetActive(true);
            if (_target)
                _targetInstance = Instantiate(_target);

        }

        if (_breakObject)
            _breakObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(_intactObject);
        Destroy(_targetInstance);
        _breakObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (_intactObject)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity, _layer))
            {
                _targetInstance.transform.position = new Vector3(hit.point.x, hit.point.y - _offset, hit.point.z);
                Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.red);
            }
        }
    }
}
