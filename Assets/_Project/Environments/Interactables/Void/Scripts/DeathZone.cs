using UnityEngine;

[RequireComponent(typeof(SceneLoader))]
public class DeathZone : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    void Awake()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerManager.Instance)
            {
                PlayerManager.Instance.SoulCounter = 0;
                
                if (PlayerManager.Instance.Checkpoint != null)
                {
                    PlayerManager.Instance.gameObject.transform.position = PlayerManager.Instance.Checkpoint.position;
                    _sceneLoader.LoadSceneByName(PlayerManager.Instance.Checkpoint.scene);
                }
            }

        }
    }
}
