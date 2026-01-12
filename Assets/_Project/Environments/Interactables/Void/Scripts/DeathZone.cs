using System.Collections;
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
            IsometricCameraManager.Instance.IsometricCamera.Follow = null;

            if (PlayerManager.Instance)
            {
                UIManager.Instance.Show(PanelType.GameOver);
                StartCoroutine(StopPlayerFalling());
                // PlayerManager.Instance.SoulCounter = 0;

                // if (PlayerManager.Instance.Checkpoint != null)
                // {
                //     PlayerManager.Instance.gameObject.transform.position = PlayerManager.Instance.Checkpoint.position;
                //     _sceneLoader.LoadSceneByName(PlayerManager.Instance.Checkpoint.scene);
                // }
            }

        }
    }

    private IEnumerator StopPlayerFalling()
    {
        yield return new WaitForSeconds(1.0f);
        PlayerManager.Instance.CharacterController.enabled = false;
    }
}
