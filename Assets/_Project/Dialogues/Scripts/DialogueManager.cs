using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#region Dialogue Data Structures
[System.Serializable]
public class DialogueChoice
{
    public string text;
    public int next_dialogue;
}

[System.Serializable]
public class DialogueEntry
{
    public int id;
    public string speaker;
    public string text;
    public bool visited;
    public string alternative_text;
    public List<DialogueChoice> choices;
    public List<string> required_achievements;
    public List<string> new_achievements;
    public List<string> effects;
    public int next_dialogue;
    public bool is_end;
}

[System.Serializable]
public class DialogueData
{
    public List<DialogueEntry> dialogues;
}
#endregion

public class DialogueManager : MonoBehaviour
{
    static DialogueManager _instance;
    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ERROR (DialogueManager): No Instance found.");
            }
            return _instance;
        }
    }
    private enum Language { EN, FR }

    [Header("Dialogue Language")]
    [SerializeField] private Language _currentLanguage = Language.EN;

    [Header("Dialogue UI")]
    public GameObject DialoguePanel;
    public GameObject DialogueBox;
    public Image DialogueImage;
    public TMP_Text DialogueText;

    [Header("Choices UI")]
    public GameObject ChoiceBox;
    public List<Button> ChoiceButtons;


    [Header("Player Dialogue Interaction")]
    public bool CanInteract;
    public bool DialogueIsRunning;
    private float _restartInteractionCooldown = 0.2f;
    [HideInInspector] public bool PlayerChose;

    public void CanInteractCooldownCoroutine()
    {
        StartCoroutine(CanInteractCooldown());
    }
    private IEnumerator CanInteractCooldown()
    {
        yield return new WaitForSeconds(_restartInteractionCooldown);
        CanInteract = true;
    }

    // Dialogues data
    private float _displayLetterSpeed = 0.02f;
    private List<DialogueEntry> _dialogues = new();
    [HideInInspector] public DialogueEntry CurrentDialogue;

    // Load images
    private Dictionary<string, Sprite> _speakerPortraits = new();

    // State Machine
    public StateMachine DialogueStateMachine;
    // States
    private DialogueIdleState _idleState;
    private DialoguePassiveState _passiveState;
    private DialogueChoiceState _choiceState;
    // State Getter
    public DialogueIdleState IdleState => _idleState;
    public DialoguePassiveState PassiveState => _passiveState;
    public DialogueChoiceState ChoiceState => _choiceState;


    private void Awake()
    {
        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (DialogueManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        _instance = this;

        _idleState = new DialogueIdleState();
        _passiveState = new DialoguePassiveState();
        _choiceState = new DialogueChoiceState();
    }

    private void Start()
    {
        DialogueStateMachine = new StateMachine();
        DialogueStateMachine.Initialize(IdleState);

        LoadSpeakerPortraits();
        LoadDialogues();
    }

    private void Update()
    {
        DialogueStateMachine.CurrentState.Update();
    }
    // 

    private string GetLanguage(Language language) => language == Language.EN ? "en" : "fr";
    private string GetDialoguePath() => Path.Combine(Application.streamingAssetsPath, $"dialogue_{GetLanguage(_currentLanguage)}.json");

    private void LoadSpeakerPortraits()
    {
        Sprite[] portraits = Resources.LoadAll<Sprite>("Dialogue/Portraits");
        foreach (var sprite in portraits)
        {
            _speakerPortraits[sprite.name.ToLower()] = sprite;
            Debug.Log(sprite.name.ToLower());
        }

        Debug.Log($"Loaded {_speakerPortraits.Count} portraits");
    }

    private void LoadDialogues()
    {
        string path = GetDialoguePath();

        if (File.Exists(path))
        {
            string jsonDialogue = File.ReadAllText(path);
            _dialogues = JsonUtility.FromJson<DialogueData>(jsonDialogue).dialogues;
        }
        else
        {
            Debug.LogError($"Cannot find language file at: {path}");
        }
    }

    public void StartDialogue(int startId)
    {
        CurrentDialogue = _dialogues.Find(d => d.id == startId);
        DialogueIsRunning = true;
    }

    public bool DialogueHasChoices() => CurrentDialogue.choices != null && CurrentDialogue.choices.Count > 0;

    public bool NextDialogue()
    {
        if (CurrentDialogue.next_dialogue >= 0)
        {
            CurrentDialogue = _dialogues.Find(d => d.id == CurrentDialogue.next_dialogue);
            return true;
        }
        return false;
    }

    public int GetCurrentDialogueID()
    {
        return CurrentDialogue.id;
    }

    public bool IsEndDialogue()
    {
        return CurrentDialogue.is_end;
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        PlayerChose = true;
        ChoiceBox.SetActive(false);

        int nextId = CurrentDialogue.choices[choiceIndex].next_dialogue;
        CurrentDialogue = _dialogues.Find(d => d.id == nextId);
    }

    public bool PlayerCanAccessNextDialogue()
    {
        DialogueEntry nextDialogue = _dialogues.Find(d => d.id == CurrentDialogue.next_dialogue);

        if (nextDialogue == null) return false;

        if (nextDialogue.required_achievements == null || nextDialogue.required_achievements.Count == 0)
            return true;

        foreach (string achievement in nextDialogue.required_achievements)
        {
            if (!PlayerManager.Instance.HasAchievement(achievement))
                return false;
        }
        return true;
    }

    public void AssignNewAchievementToPlayer()
    {
        if (CurrentDialogue.new_achievements != null)
            foreach (string achievement in CurrentDialogue.new_achievements)
            {
                PlayerManager.Instance.AddAchievement(achievement);
                Debug.Log($"New Achivement unlocked : {achievement}");
            }
    }


    #region "Dialogue Display Logic"
    public void DisplayDialogueCoroutine()
    {
        StartCoroutine(DisplayDialogue());
    }

    public void DisplayDialogueWithChoiceCoroutine()
    {
        StartCoroutine(DisplayDialogueWithChoice());
    }

    private IEnumerator DisplayDialogue()
    {
        DialogueBox.SetActive(true);
        DialogueImage.sprite = _speakerPortraits[CurrentDialogue.speaker];

        string textToDisplay = CurrentDialogue.text;
        if (CurrentDialogue.visited && CurrentDialogue.alternative_text != null)
            textToDisplay = CurrentDialogue.alternative_text;

        yield return StartCoroutine(DisplayText(textToDisplay));
        CurrentDialogue.visited = true;
        CanInteract = true;
    }


    public IEnumerator DisplayDialogueWithChoice()
    {
        DialogueBox.SetActive(true);
        yield return StartCoroutine(DisplayDialogue());

        ChoiceBox.SetActive(true);
        foreach (var btn in ChoiceButtons)
            btn.gameObject.SetActive(false);

        for (int i = 0; i < CurrentDialogue.choices.Count && i < ChoiceButtons.Count; i++)
        {
            var button = ChoiceButtons[i];
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TMP_Text>().text = CurrentDialogue.choices[i].text;

            int index = i; // we need to keep that value (because "i" passed directly to the listener is a ref)
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    public IEnumerator DisplayText(string text)
    {
        foreach (char letter in text)
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(_displayLetterSpeed);
        }
    }
    #endregion
}
