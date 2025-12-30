using System.Collections;
using UnityEngine;

public class PressurePlateAction : MonoBehaviour
{
    [SerializeField] private GameObject _actionTarget;
    [SerializeField] private string _achievementNameToForceAction;
    private GameObject _actionSource;
    private bool _deactivate;

    //Sounds
    [Header("Sounds")]
    public AudioClip DoorOpening;

    void Update()
    {
        if (PlayerManager.Instance.HasAchievement(_achievementNameToForceAction))
        {
            StartCoroutine(Action());
            return;
        }
        
        if (_actionSource && !_deactivate)
        {
            Vector3 distance = (_actionSource.transform.position - transform.position).normalized;
            if (Vector3.Dot(distance, transform.up) > 0.98)
            {
                StartCoroutine(Activate());
                StartCoroutine(Action());
                _deactivate = true;
            }
        }
    }

    private IEnumerator Activate()
    {
        yield return new WaitForSeconds(1.0f);
        transform.Translate(Vector3.down * 0.2f);
    }

    private IEnumerator Action()
    {
        if (_actionTarget)
        {
            Animator animator = _actionTarget.GetComponent<Animator>();
            if (animator)
            {
                yield return new WaitForSeconds(1.0f);
                animator.SetBool("IsOpen", true);
                SoundManager.Instance.PlaySoundClip(DoorOpening, transform);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            if (_actionSource == null)               
            _actionSource = collision.gameObject;
            
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            _actionSource = null;
        }
    }
}
