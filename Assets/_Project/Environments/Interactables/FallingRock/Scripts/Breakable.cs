using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject _intactObject;
    [SerializeField] private GameObject _breakObject;
    [SerializeField] private GameObject _target;
    private GameObject _targetInstance;
    private float _offset = 0.95f;
    private Collider _collider;
    [SerializeField] private LayerMask _layer;
    void Awake()
    {
        _collider = GetComponent<Collider>();

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

    void Update()
    {
        StartCoroutine(DeleteRocks());
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(_intactObject);
        Destroy(_targetInstance);
        _breakObject.SetActive(true);

        ApplyDamage(collision);
        _collider.enabled = false;
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

    void ApplyDamage(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.HealthManager.Hurt();
        }

        //TODO : Same thing for the enemy here.
    }

    private IEnumerator DeleteRocks()
    {
        yield return new WaitForSeconds(7.0F);
        Destroy(gameObject);
    }
}
