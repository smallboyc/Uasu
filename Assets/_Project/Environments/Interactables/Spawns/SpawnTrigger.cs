using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class SpawnTrigger : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    [SerializeField] string _sceneToLoad;
    [SerializeField] Transform _newSpawnTransform;

    void Awake()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerManager.Instance.IsTransitioning)
        {
            Debug.Log("ENTER!");
            PlayerManager.Instance.IsTransitioning = true;

            if (_newSpawnTransform)
                PlayerManager.Instance.gameObject.transform.position = _newSpawnTransform.position;

            if (_sceneToLoad != null)
            {
                _sceneLoader.LoadSceneByName(_sceneToLoad);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.Instance.IsTransitioning = false;
            Debug.Log("EXIT!");
        }
    }
}
