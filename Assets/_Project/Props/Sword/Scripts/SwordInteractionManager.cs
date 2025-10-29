using System.Collections;
using UnityEngine;

public class SwordInteractionManager : MonoBehaviour
{
    [SerializeField] private Collider _pickupCollider;
    private Transform _handHolder;
    private Transform _backHolder;
    private bool _playerCanGetSword;
    private bool _playerHasSword;
    private bool _isInHand;
    private bool _isInteractionLocked;

    public void FindPlayerHolders()
    {
        if (_handHolder == null)
            _handHolder = GameObject.FindGameObjectWithTag("HandHolder").transform;

        if (_backHolder == null)
            _backHolder = GameObject.FindGameObjectWithTag("BackHolder").transform;
    }

    public void EnableSwordInteraction()
    {
        if (!_playerHasSword)
        {
            if (PlayerInputManager.Instance.InteractPressed && _playerCanGetSword) GetSword();
        }
        else
        {
            if (PlayerInputManager.Instance.ToggleWeaponPressed && !_isInteractionLocked) StartCoroutine(ToggleWeapon());
        }
    }

    private void GetSword()
    {
        _pickupCollider.enabled = false;
        if (_backHolder == null || _handHolder == null)
        {
            Debug.LogError($"[{name}] PlayerInteractionManager error: Back/Hand Holder is null.");
            _isInteractionLocked = false;
        }
        AttachTo(_handHolder);
        _playerHasSword = true;
    }
    private IEnumerator ToggleWeapon()
    {
        if (_isInHand)
            AttachTo(_backHolder);
        else
            AttachTo(_handHolder);

        _isInteractionLocked = true;
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
        if (other.CompareTag("Player"))
        {
            _playerCanGetSword = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCanGetSword = false;
        }
    }
}
