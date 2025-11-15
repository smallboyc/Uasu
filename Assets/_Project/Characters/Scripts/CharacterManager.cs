using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterManager : MonoBehaviour
{
    [HideInInspector] public CharacterController CharacterController;
    
    // [Header("Health")]
    // [SerializeField] protected int _health = 3;
    protected virtual void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    protected virtual void Start()
    {
        // -> Next : we will move this block to a higher level Manager like Scene/Game
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        //
    }

    protected virtual void Update()
    {

    }
}