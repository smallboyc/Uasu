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

    [Header("Panels")]
    public GameObject GameOverPanel;

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

        GameOverPanel = transform.Find("Canvas/GameOverPanel").gameObject;
        GameOverPanel.SetActive(false);
    }


    public void GameOver()
    {
        Button button = GameOverPanel.GetComponentInChildren<Button>();
        button.Select();
        GameOverPanel.SetActive(true);
    }



}
