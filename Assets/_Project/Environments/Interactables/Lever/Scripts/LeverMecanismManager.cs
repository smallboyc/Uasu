using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeverMecanismManager : MonoBehaviour
{
    private enum State { MissingLever, LeverAttached }
    private State _currentState = State.MissingLever;
    [SerializeField] private Collider _interactCollider;
    private bool _playerInRange;
    private Transform _leverPosition;
    private Animator _leverMecanismAnimator;
    [SerializeField] List<GameObject> _targetBridges;

    //Sounds
    [Header("Sounds")]
    public AudioClip HitLever;
    public AudioClip FallingBridge;

    void Awake()
    {
        _leverPosition = transform.Find("LeverObjectPosition");
        if (_leverPosition == null)
        {
            Debug.LogError($"[{name}] : Cannot find Lever Position object.");
        }

        _leverMecanismAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        if (_playerInRange)
        {
            if (_currentState == State.MissingLever && PlayerInputManager.Instance.InteractPressed && PlayerManager.Instance.Collectables.ContainsKey(PlayerManager.CollectableItems.Lever)) ReadyToActivate();
            if (_currentState == State.LeverAttached && PlayerInputManager.Instance.AttackPressed) Activate();
        }

    }

    private void ReadyToActivate()
    {
        _currentState = State.LeverAttached;
        _interactCollider.enabled = false;

        if (PlayerManager.Instance.BackHolder == null || PlayerManager.Instance.HandHolder == null)
        {
            Debug.LogError($"[{name}] PlayerInteractionManager error: Back/Hand Holder is null.");
        }

        AttachTo(_leverPosition);

        PlayerManager.Instance.Collectables.Remove(PlayerManager.CollectableItems.Lever);
    }

    private void Activate()
    {
        SoundManager.Instance.PlaySoundClip(HitLever, transform);
        _leverMecanismAnimator.SetBool("IsActivated", true);
        
    }

    private void UnlockBridge() //Call in the end of the lever animation.
    {
        if (_targetBridges.Count != 0)
        {
            foreach (GameObject bridge in _targetBridges)
            {
                bridge.transform.Find("BridgePlate").GetComponent<Animator>().SetBool("IsOpen", true);
                SoundManager.Instance.PlaySoundClip(FallingBridge, transform);
            }
        }
    }

    private void AttachTo(Transform newParent)
    {
        Transform leverTransform = PlayerManager.Instance.Collectables[PlayerManager.CollectableItems.Lever].transform;
        leverTransform.SetParent(newParent);
        leverTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    // TRIGGER //
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _playerInRange = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}
