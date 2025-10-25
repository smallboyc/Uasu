using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController _characterController;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        _characterController = GetComponent<CharacterController>();
    }

    protected virtual void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }

    protected virtual void Update()
    {

    }
}