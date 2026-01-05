using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform _checkpointTransform;
    private bool _canInteract;
    private bool _set;

    //Sounds
    [Header("Sounds")]
    public AudioClip Checkpoint;

    public void SetCheckpoint()
    {
        PlayerManager.Instance.Checkpoint.position = _checkpointTransform.position;
        PlayerManager.Instance.Checkpoint.scene = SceneManager.GetActiveScene().name;
        _set = true;
    }

    public void Update()
    {
        if (_set)
            return;
            
        if (_canInteract && PlayerInputManager.Instance.InteractPressed)
        {
            SoundManager.Instance.PlaySoundClip(Checkpoint, transform);
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
