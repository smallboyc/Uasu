using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{
    [SerializeField] private SorcererElementsManager.ElementType _elementType;

    void Start()
    {
        if (SorcererElementsManager.Instance.CollectedElements().Contains(_elementType) || !PlayerManager.Instance.HasAchievement("GOOD_LUCK_LITTLE_HERO"))
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SorcererElementsManager.Instance.AddTask(_elementType);
            Destroy(gameObject);
        }
    }
}
