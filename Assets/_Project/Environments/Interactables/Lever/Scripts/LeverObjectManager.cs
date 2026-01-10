using System.Collections;
using UnityEngine;

public class LeverObjectManager : MonoBehaviour
{
    [SerializeField] private Collider _pickupCollider;
    private bool _playerCanGetLever;

    //Sounds
    [Header("Sounds")]
    public AudioClip TakeLever;


    void Update()
    {
        if (PlayerInputManager.Instance)
        {
            if (PlayerInputManager.Instance.InteractPressed && _playerCanGetLever) GetLever();

            if (PlayerManager.Instance.Collectables.ContainsKey(PlayerManager.CollectableItems.Lever))
                if (PlayerManager.Instance.HoldSword)
                    AttachTo(PlayerManager.Instance.BackHolder);
                else
                    AttachTo(PlayerManager.Instance.HandHolder);
        }
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
        SoundManager.Instance.PlaySoundClip(TakeLever, transform);
        PlayerManager.Instance.Collectables.Add(PlayerManager.CollectableItems.Lever, gameObject);
        UIManager.Instance.Hide(PanelType.Help);
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
            HelpManager.Instance.SetHelpText("INTERACT");
            UIManager.Instance.Show(PanelType.Help);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerCanGetLever = false;
            UIManager.Instance.Hide(PanelType.Help);
        }
    }
}
