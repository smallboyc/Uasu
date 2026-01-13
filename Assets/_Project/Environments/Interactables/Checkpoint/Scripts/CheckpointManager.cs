using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform _checkpointTransform;
    [SerializeField] private int _checkpointID;
    private bool _canInteract;
    private bool _set;

    //Sounds
    [Header("Sounds")]
    public AudioClip Checkpoint;
    public ParticleSystem CheckpointParticles;

    public void Start()
    {
        if (PlayerManager.Instance.Checkpoint.id == _checkpointID)
            _set = true;
    }

    public void SetCheckpoint()
    {
        PlayerManager.Instance.Checkpoint.position = _checkpointTransform.position;
        PlayerManager.Instance.Checkpoint.id = _checkpointID;
        PlayerManager.Instance.Checkpoint.scene = SceneManager.GetActiveScene().name;
        _set = true;
    }

    public void Update()
    {
        if (_set)
            return;

        if (_canInteract && PlayerInputManager.Instance.InteractPressed)
        {
            if (SoundManager.Instance)
                SoundManager.Instance.PlaySoundClip(Checkpoint, transform);
            Debug.Log("Change checkpoint");
            PlayCheckpointParticles();
            SetCheckpoint();
            UIManager.Instance.Hide(PanelType.Help);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = true;
            if (!_set)
            {
                HelpManager.Instance.SetHelpText("INTERACT to activate Checkpoint");
                UIManager.Instance.Show(PanelType.Help);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _canInteract = false;
            UIManager.Instance.Hide(PanelType.Help);
        }
    }

    void PlayCheckpointParticles()
    {
       CheckpointParticles.Play();
    }
}
