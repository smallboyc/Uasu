using UnityEngine;

//TODO : Create a PropsManager to centralized interaction and use inheritance.
[RequireComponent(typeof(SwordInteractionManager))]
public class SwordManager : MonoBehaviour
{
    SwordInteractionManager _swordInteractionManager;
    void Awake()
    {
        _swordInteractionManager = GetComponent<SwordInteractionManager>();
    }

    void Start()
    {
        _swordInteractionManager.FindPlayerHolders();
    }

    void Update()
    {
        if (PlayerManager.Instance)
            _swordInteractionManager.EnableSwordInteraction();
    }
}
