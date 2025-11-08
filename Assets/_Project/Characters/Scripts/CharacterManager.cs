using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterHealthManager))]
public abstract class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController CharacterController;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        CharacterController = GetComponent<CharacterController>();
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