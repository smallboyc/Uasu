using System.Collections.Generic;
using UnityEngine;



public class Element : MonoBehaviour
{
    [SerializeField] private SorcererElementsManager.ElementType _elementType;

    //Sounds
    [Header("Sounds")]
    public AudioClip ItemCatch;
    public ParticleSystem ElementParticles;

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
            PlayElementParticles();
            Destroy(gameObject);
        }
    }

    void PlayElementParticles()
    {
        ParticleSystem particles = Instantiate(ElementParticles, transform.position, Quaternion.identity);
        particles.Play();
        Destroy(particles.gameObject, particles.main.duration);
    }

}
