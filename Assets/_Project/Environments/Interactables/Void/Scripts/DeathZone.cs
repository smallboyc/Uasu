using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class DeathZone : MonoBehaviour
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
            PlayerManager.Instance.gameObject.transform.position = PlayerManager.Instance.Checkpoint.position;
            if (_sceneToLoad != null)
            {
                _sceneLoader.LoadSceneByName(_sceneToLoad);
            }

        }
    }
}
