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
    Options,
    GameOver,
    Transition,
    Video,
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

    [SerializeField] private KeyItemUIIcon keyItemUIIcon;

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
            Instance.HideAll();
            Instance.Show(PanelType.SplashScreen);
            Debug.Log("Splash Screen Loaded!");
            return;
        }

        if (scene.name == "MainMenu")
        {
            Instance.HideAll();
            Instance.Show(PanelType.MainMenu);
            Debug.Log("Main Menu Screen Loaded!");
            return;
        }

        if (scene.name.Contains("Cinematic"))
        {
            Instance.Show(PanelType.Video);
            return;
        }

        if (scene.name == "Level_01_Main" && GameObject.FindWithTag("Player"))
{
            DialogueManager.Instance.DialogueIsActive = true;

            Instance.HideAll(keepHUD: true); 
            Instance.Show(PanelType.Transition);
            Instance.Show(PanelType.HUD);

            Debug.Log(scene.name);
            Debug.Log("Player HUD Loaded!");
         return;
}

    }

    public void ShowOptions()
    {
        panels[PanelType.Options].Show();
    }

    public void Show(PanelType type)
    {
        panels[type].Show();
    }

    public void Hide(PanelType type)
    {
        Debug.Log("NON");
        panels[type].Hide();
    }

 public void HideAll(bool keepHUD = false)
{
    foreach (var kvp in panels)
    {
        if (keepHUD && kvp.Key == PanelType.HUD)
            continue;

        kvp.Value.Hide();
    }
}


    public void HideAllExcept(PanelType type)
    {
        foreach (var kvp in panels)
        {
            if (kvp.Value.type == type)
                continue;
            kvp.Value.Hide();
        }
    }

    public UIPanel GetPanel(PanelType type)
    {
        return panels[type];
    }

        public void ShowSwordIcon()
{
    if (keyItemUIIcon == null)
    {
        Debug.LogError("KeyItemsUI no está asignado en UIManager");
        return;
    }

    keyItemUIIcon.ShowSwordIcon();
}
}
