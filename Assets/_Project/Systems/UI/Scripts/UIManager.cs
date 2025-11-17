using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ERROR (UIManager): No Instance found.");
            }
            return _instance;
        }
    }

    private GameObject _gameOverPanel;

    private void Awake()
    {
        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (UIManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _gameOverPanel = transform.Find("Canvas/GameOverPanel").gameObject;
        _gameOverPanel.SetActive(false);
    }


    public void GameOver()
    {
        Button button = _gameOverPanel.GetComponentInChildren<Button>();
        button.Select();
        _gameOverPanel.SetActive(true);
    }



}
