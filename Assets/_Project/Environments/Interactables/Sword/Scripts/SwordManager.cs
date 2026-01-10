using System.Collections;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    [SerializeField] private Collider _pickupCollider;

    private bool _playerCanGetSword;
    private bool _isInteractionLocked;

    void Update()
    {
        if (!PlayerManager.Instance)
            return;

        if (!PlayerManager.Instance.HasAchievement("THE_ARTIFACT"))
            return;

        if (!PlayerManager.Instance.Collectables.ContainsKey(PlayerManager.CollectableItems.Sword))
        {
            if (PlayerInputManager.Instance.InteractPressed && _playerCanGetSword)
            {
                GetSword();
                PlayerManager.Instance.AddAchievement("THE_SWORD");
                UIManager.Instance.Hide(PanelType.Help);
            }
        }
        else
        {
            if (PlayerInputManager.Instance.ToggleWeaponPressed && !_isInteractionLocked) StartCoroutine(ToggleWeapon());
        }
    }

    private void GetSword()
    {
        _pickupCollider.enabled = false;
        if (PlayerManager.Instance.BackHolder == null || PlayerManager.Instance.HandHolder == null)
        {
            Debug.LogError($"[{name}] PlayerInteractionManager error: Back/Hand Holder is null.");
            _isInteractionLocked = false;
        }

        AttachTo(PlayerManager.Instance.HandHolder);

        PlayerManager.Instance.Collectables.Add(PlayerManager.CollectableItems.Sword, gameObject);

        UIManager.Instance.Hide(PanelType.Talisman);
        UIManager.Instance.Show(PanelType.Weapon);
    }
    private IEnumerator ToggleWeapon()
    {
        if (PlayerManager.Instance.HoldSword)
            AttachTo(PlayerManager.Instance.BackHolder);
        else
            AttachTo(PlayerManager.Instance.HandHolder);

        _isInteractionLocked = true;
        yield return new WaitForSeconds(0.3f);
        _isInteractionLocked = false;
    }


    private void AttachTo(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        PlayerManager.Instance.HoldSword = newParent == PlayerManager.Instance.HandHolder;
    }

    // TRIGGER //
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCanGetSword = true;
            if (!PlayerManager.Instance.HasAchievement("THE_ARTIFACT"))
                HelpManager.Instance.SetHelpText("I can't get this Sword.");
            else
                HelpManager.Instance.SetHelpText("INTERACT to get the Sword");
            UIManager.Instance.Show(PanelType.Help);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCanGetSword = false;
            UIManager.Instance.Hide(PanelType.Help);
        }
    }


}
