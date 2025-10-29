using UnityEngine;

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
        _swordInteractionManager.EnableSwordInteraction();
    }
}
