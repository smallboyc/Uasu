using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class SorcererElementsManager : MonoBehaviour
{
    public static SorcererElementsManager Instance;

    void Awake()
    {
        //Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (ElementManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public enum ElementType { Flower, Mirror, Feather };
    [SerializeField] private string _achievementName = "THE_SORCERER_ELEMENTS";
    [SerializeField] private List<ElementType> _collectedElements = new();

    public List<ElementType> CollectedElements() => _collectedElements;

    public void AddTask(ElementType element)
    {
        _collectedElements.Add(element);
        if (IsEndTask())
        {
            PlayerManager.Instance.AddAchievement(_achievementName);
        }
    }

    private bool IsEndTask()
    {
        if (!_collectedElements.Contains(ElementType.Flower))
            return false;

        if (!_collectedElements.Contains(ElementType.Mirror))
            return false;

        if (!_collectedElements.Contains(ElementType.Feather))
            return false;

        return true;
    }
}
