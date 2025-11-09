using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController CharacterController;
    [SerializeField] protected int _health;
    protected virtual void Awake()
    {
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