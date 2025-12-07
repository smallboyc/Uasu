using System.Collections;
using UnityEngine;

public class LeverObjectManager : MonoBehaviour
{
    [SerializeField] private Collider _pickupCollider;
    private bool _playerCanGetLever;

    void Update()
    {

        if (PlayerInputManager.Instance.InteractPressed && _playerCanGetLever) GetLever();

        if (PlayerManager.Instance.Collectables.ContainsKey(PlayerManager.CollectableItems.Lever))
            if (PlayerManager.Instance.HoldSword)
                AttachTo(PlayerManager.Instance.BackHolder);
            else
                AttachTo(PlayerManager.Instance.HandHolder);
    }

    private void GetLever()
    {
        _pickupCollider.enabled = false;
        _playerCanGetLever = false;
        if (PlayerManager.Instance.BackHolder == null || PlayerManager.Instance.HandHolder == null)
        {
            Debug.LogError($"[{name}] PlayerInteractionManager error: Back/Hand Holder is null.");
        }

        AttachTo(PlayerManager.Instance.HandHolder);

        PlayerManager.Instance.Collectables.Add(PlayerManager.CollectableItems.Lever, gameObject);
    }

    private void AttachTo(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    // TRIGGER //
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCanGetLever = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCanGetLever = false;
        }
    }
}
