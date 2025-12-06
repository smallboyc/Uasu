using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform _checkpointTransform;
    private bool _canInteract;
    private bool _set;

    public void SetCheckpoint()
    {
        PlayerManager.Instance.Checkpoint = _checkpointTransform;
        _set = true;
    }

    public void Update()
    {
        if (_set)
            return;
            
        if (_canInteract && PlayerInputManager.Instance.InteractPressed)
        {
            Debug.Log("Change checkpoint");
            SetCheckpoint();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = false;
        }
    }
}
