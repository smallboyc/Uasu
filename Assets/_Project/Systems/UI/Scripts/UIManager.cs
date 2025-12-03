using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PanelType
{
    SplashScreen,
    MainMenu,
    HUD,
    Dialogue,
    Pause,
    GameOver
}

[System.Serializable]
public class UIPanel
{
    public PanelType type;
    public GameObject panelObject;

    public void Show() => panelObject.SetActive(true);
    public void Hide() => panelObject.SetActive(false);
}


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private List<UIPanel> panelsList = new();
    private Dictionary<PanelType, UIPanel> panels;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        panels = new Dictionary<PanelType, UIPanel>();
        foreach (var panel in panelsList)
        {
            panels[panel.type] = panel;
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //If the player exists, then we need to (re)-activate his character controller.
        if (PlayerManager.Instance)
        {
            PlayerManager.Instance.CharacterController.enabled = true;
        }

        if (scene.name == "SplashScreen")
        {
            Instance.ShowOnly(PanelType.SplashScreen);
            Debug.Log("Splash Screen Loaded!");
            return;
        }

        if (scene.name == "MainMenu")
        {
            Instance.ShowOnly(PanelType.MainMenu);
            Debug.Log("Main Menu Screen Loaded!");
            return;
        }

        Instance.HideAll();
    }

    public void Show(PanelType type)
    {
        panels[type].Show();
    }

    public void Hide(PanelType type)
    {
        panels[type].Hide();
    }

    public void ShowOnly(PanelType type)
    {
        foreach (var kvp in panels)
        {
            if (kvp.Key == type)
                kvp.Value.Show();
            else
                kvp.Value.Hide();
        }
    }

    public void HideAll()
    {
        foreach (var kvp in panels)
        {
            kvp.Value.Hide();
        }
    }

    public UIPanel GetPanel(PanelType type)
    {
        return panels[type];
    }
}
