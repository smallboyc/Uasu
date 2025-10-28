using System.Collections;
using UnityEngine;

public class SwordInteractionManager : MonoBehaviour
{
    [SerializeField] private Collider _pickupCollider;
    [SerializeField] private Collider _hitboxCollider;
    private string _playerTag = "Player";
    private bool _playerCanGetSword;
    private bool _playerHasSword;
    [SerializeField] private Transform _handHolder;
    [SerializeField] private Transform _backHolder;
    private bool _isInHand;
    private bool _isInteractionLocked = false;

    void Awake()
    {
        _pickupCollider.enabled = true;
        _hitboxCollider.enabled = false;
    }


    void Update()
    {
        if (PlayerInputManager.Instance.interactPressed && !_isInteractionLocked)
        {
            StartCoroutine(HandleInteraction());
        }
    }

    private IEnumerator HandleInteraction()
    {
        _isInteractionLocked = true;

        if (_playerCanGetSword)
        {
            _playerCanGetSword = false;
            _pickupCollider.enabled = false;
            _hitboxCollider.enabled = true;
            if (_backHolder == null || _handHolder == null)
            {
                Debug.LogError($"[{name}] PlayerInteractionManager error: Back/Hand Holder is null.");
                _isInteractionLocked = false;
                yield break;
            }
            AttachTo(_handHolder);
            _playerHasSword = true;
        }
        else if (_playerHasSword)
        {
            if (_isInHand)
                AttachTo(_backHolder);
            else
                AttachTo(_handHolder);
        }
        yield return new WaitForSeconds(0.3f);

        _isInteractionLocked = false;
    }


    private void AttachTo(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        _isInHand = newParent == _handHolder;
    }

    // TRIGGER //
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _playerCanGetSword = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_playerTag))
        {
            _playerCanGetSword = false;
        }
    }
}
