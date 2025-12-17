using System.Collections;
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
            PlayerManager.Instance.CharacterController.enabled = false;
            PlayerManager.Instance.PlayerStateMachine.ChangeState(PlayerManager.Instance.IdleState);
            PlayerManager.Instance.IsTransitioning = true;
            TransitionPanelManager.Instance.NewTransition(TransitionPanelManager.TransitionType.FadeOut, TransitionPanelManager.TransitionColor.Black);
            StartCoroutine(LoadSceneTransition());
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

    private IEnumerator LoadSceneTransition()
    {
        yield return new WaitForSeconds(2.0f);
        if (_newSpawnTransform)
            PlayerManager.Instance.gameObject.transform.position = _newSpawnTransform.position;

        if (_sceneToLoad != null)
        {
            _sceneLoader.LoadSceneByName(_sceneToLoad);
        }
    }
}
