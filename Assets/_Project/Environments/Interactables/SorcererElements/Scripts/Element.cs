using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{
    [SerializeField] private SorcererElementsManager.ElementType _elementType;

    //Sounds
    [Header("Sounds")]
    public AudioClip ItemCatch;

    void Start()
    {
        if (SorcererElementsManager.Instance.CollectedElements().Contains(_elementType) || !PlayerManager.Instance.HasAchievement("GOOD_LUCK_LITTLE_HERO"))
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SoundManager.Instance)
                SoundManager.Instance.PlaySoundClip(ItemCatch, transform);
            SorcererElementsManager.Instance.AddTask(_elementType);
            Destroy(gameObject);
        }
    }
}
