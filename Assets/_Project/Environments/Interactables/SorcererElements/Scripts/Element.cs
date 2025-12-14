using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{
    [SerializeField] private SorcererElementsManager.ElementType _elementType;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SorcererElementsManager.Instance.AddTask(_elementType);
            Destroy(gameObject);
        }
    }
}
