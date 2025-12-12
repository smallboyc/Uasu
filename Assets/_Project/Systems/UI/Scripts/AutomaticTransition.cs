using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutomaticTransition : MonoBehaviour
{
    [SerializeField] private float _waitTime = 5.0f;
    [SerializeField] private string _sceneName;
    void Start()
    {
        StartCoroutine(TransitionScene());
    }

    private IEnumerator TransitionScene()
    {
        yield return new WaitForSeconds(_waitTime);
        SceneManager.LoadScene(_sceneName);
    }
}
