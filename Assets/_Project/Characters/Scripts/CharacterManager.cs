using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterHealthManager))]
public class CharacterManager : MonoBehaviour
{
    [HideInInspector] protected CharacterController characterController;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        characterController = GetComponent<CharacterController>();
        
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