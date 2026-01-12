using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PanelType
{
    SplashScreen,
    MainMenu,
    HUD_Souls,
    HUD_Health,
    Dialogue,
    Pause,
    Options,
    GameOver,
    Transition,
    Cinematic,
    Weapon,
    Talisman,
    Loading,
    Help,
    Controller,
    Credits,
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

    [Header("UI Sounds")]
    [SerializeField] private AudioClip uiClick;
    [SerializeField] private AudioClip uiHover;
    [SerializeField] private AudioClip uiNavigate;

    private bool _canPause;
    public bool GamePaused { get; private set; }
    public bool IsLoading;

    public static UIManager Instance { get; private set; }

    [SerializeField] private List<UIPanel> panelsList = new();
    private Dictionary<PanelType, UIPanel> panels;

    private bool _talismanPanelActive;
    private bool _loadFromMainMenu;


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

    void Update()
    {
        if (!PlayerInputManager.Instance)
            return;

        if (_canPause && (Input.GetKeyDown(KeyCode.Escape) || PlayerInputManager.Instance.PausePressed))
        {
            PauseCooldownCoroutine();

            if (!GamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        if (PlayerManager.Instance && !_talismanPanelActive && PlayerManager.Instance.HasAchievement("THE_ARTIFACT"))
        {
            _talismanPanelActive = true;
            Instance.Show(PanelType.Talisman);
            return;
        }
    }

    void PauseGame()
    {
        GamePaused = true;
        panels[PanelType.Pause].Show();
    }

    void ResumeGame()
    {
        GamePaused = false;
        panels[PanelType.Options].Hide();
        panels[PanelType.Pause].Hide();
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

        if (scene.name == "SplashScreen" || scene.name == "MainMenu")
            _canPause = false;
        else
            _canPause = true;

        if (scene.name == "SplashScreen")
        {
            Instance.HideAll();
            Instance.Show(PanelType.SplashScreen);
            Debug.Log("Splash Screen Loaded!");
            return;
        }

        if (scene.name == "MainMenu")
        {
            _loadFromMainMenu = true;
            Instance.HideAll();
            Instance.Show(PanelType.MainMenu);
            Debug.Log("Main Menu Screen Loaded!");
            return;
        }

        if (scene.name.Contains("Cinematic"))
        {
            Instance.Show(PanelType.Cinematic);
            return;
        }

        if (_loadFromMainMenu && scene.name == "Level_01_Main")
        {
            _loadFromMainMenu = false;
            DialogueManager.Instance.DialogueIsActive = true;

            Instance.HideAll();
            TransitionPanelManager.Instance.NewTransition(TransitionPanelManager.TransitionType.FadeIn, TransitionPanelManager.TransitionColor.White);
            Instance.Show(PanelType.HUD_Health);
            Instance.Show(PanelType.HUD_Souls);


            Debug.Log(scene.name);
            Debug.Log("Player HUD Loaded!");
            return;
        }

        if (PlayerManager.Instance && PlayerManager.Instance.IsTransitioning)
        {
            Loading();
        }

    }

    public void Loading()
    {
        Show(PanelType.Loading);
        IsLoading = true;
        StartCoroutine(EndTransition());
    }

    public void ShowOptions()
    {
        panels[PanelType.Options].Show();
        if (!GamePaused)
            panels[PanelType.MainMenu].Hide();
        else
            panels[PanelType.Pause].Hide();

    }

    public void HideOptions()
    {
        panels[PanelType.Options].Hide();
        if (!GamePaused)
            panels[PanelType.MainMenu].Show();
        else
            panels[PanelType.Pause].Show();
    }




    public void Show(PanelType type)
    {
        panels[type].Show();
    }

    public void Hide(PanelType type)
    {
        panels[type].Hide();
    }

    public void HideAll()
    {
        foreach (var kvp in panels)
        {
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

    private IEnumerator EndTransition()
    {
        StartCoroutine(ResetLoading());
        yield return new WaitForSeconds(LoadingPlayerManager.Instance.LoadingDuration);
        TransitionPanelManager.Instance.NewTransition(TransitionPanelManager.TransitionType.FadeIn, TransitionPanelManager.TransitionColor.Black);
        Hide(PanelType.Loading);
    }

    private IEnumerator ResetLoading()
    {
        yield return new WaitForSeconds(LoadingPlayerManager.Instance.LoadingDuration - 0.3f);
        IsLoading = false;
    }




    public void PlayUIClick()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(uiClick, transform);
    }

    public void PlayUIHover()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(uiHover, transform);
    }

    public void PlayUINavigation()
    {
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(uiHover, transform);
    }

    public void ShowController()
    {
        panels[PanelType.Controller].Show();
        panels[PanelType.Options].Hide();
    }
    public void HideController()
    {
        panels[PanelType.Controller].Hide();
        panels[PanelType.Options].Show();
    }
    public void ShowCredits()
    {
        panels[PanelType.Credits].Show();
        panels[PanelType.MainMenu].Hide();
    }
    public void HideCredits()
    {
        panels[PanelType.Credits].Hide();
        panels[PanelType.MainMenu].Show();
    }

    public void PauseCooldownCoroutine()
    {
        StartCoroutine(PauseCooldown());
    }
    public IEnumerator PauseCooldown()
    {
        _canPause = false;
        yield return new WaitForSeconds(0.1f);
        _canPause = true;
    }
}