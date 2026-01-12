using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;

[RequireComponent(typeof(SceneLoader))]
#endif

public class TriggerGameOver : MonoBehaviour
{

    private SceneLoader _sceneLoader;

    void Awake()
    {
        _sceneLoader = GetComponent<SceneLoader>();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void Respawn()
    {
        if (PlayerManager.Instance)
        {
            PlayerManager.Instance.LockManager.IsLockedOnEnemy = false;

            if (PlayerManager.Instance.Checkpoint != null)
            {
                TransitionPanelManager.Instance.NewTransition(TransitionPanelManager.TransitionType.FadeOut, TransitionPanelManager.TransitionColor.Black);
                StartCoroutine(RespawnSceneTransition());
            }
        }

    }

    public IEnumerator RespawnSceneTransition()
    {
        yield return new WaitForSeconds(5.0f);

        UIManager.Instance.Loading();

        PlayerManager.Instance.PlayerStateMachine.ChangeState(PlayerManager.Instance.IdleState);
        PlayerManager.Instance.SoulCounter = 0;
        PlayerManager.Instance.HealthManager.Health = 4;
        PlayerHealthBarManager.Instance.SetHealth(PlayerManager.Instance.HealthManager.Health);

        PlayerManager.Instance.gameObject.transform.position = PlayerManager.Instance.Checkpoint.position;
        if (IsometricCameraManager.Instance)
            IsometricCameraManager.Instance.IsometricCamera.Follow = PlayerManager.Instance.gameObject.transform;

        _sceneLoader.LoadSceneByName(PlayerManager.Instance.Checkpoint.scene);

        UIManager.Instance.Hide(PanelType.GameOver);
    }

}



