using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class EndGame : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    [SerializeField] string _sceneToLoad;

    void Awake()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.gameObject.transform.position = Vector3.zero;
            if (_sceneToLoad != null)
            {
                _sceneLoader.LoadSceneByName(_sceneToLoad);
            }

        }
    }
}
