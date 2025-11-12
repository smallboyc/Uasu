using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public List<DialogueChoice> choices;
    public List<string> required_flags;
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
    private enum Language { EN, FR }
    [SerializeField] private Language _currentLanguage = Language.EN;

    [Header("Dialogue UI")]
    public GameObject DialoguePanel;
    public GameObject DialogueBox;
    public Image DialogueImage;
    public TMP_Text DialogueText;

    [Header("Choices UI")]
    public GameObject ChoiceBox;
    public List<Button> ChoiceButtons;

    [Header("Settings")]
    public float DisplayLetterSpeed = 0.02f;
    private List<DialogueEntry> _dialogues = new();
    [HideInInspector] public DialogueEntry CurrentDialogue;

    [HideInInspector] public bool PlayerChose;
    private int _startDialogueId = 0;

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

    // Interaction Input gestion
    [SerializeField] private float _interactionCooldown = 0.2f;
    private bool _canInteract = true;
    [HideInInspector] public bool CanInteract => _canInteract;

    public IEnumerator InteractionCooldown()
    {
        _canInteract = false;
        yield return new WaitForSeconds(_interactionCooldown);
        _canInteract = true;
    }

    private void Awake()
    {
        _idleState = new DialogueIdleState(this);
        _passiveState = new DialoguePassiveState(this);
        _choiceState = new DialogueChoiceState(this);
    }

    private void Start()
    {
        DialogueStateMachine = new StateMachine();
        DialogueStateMachine.Initialize(IdleState);

        LoadDialogues();
    }

    //
    private void Update()
    {
        DialogueStateMachine.CurrentState.Update();
    }
    //

    private string GetLanguage(Language language) => language == Language.EN ? "en" : "fr";
    private string GetDialoguePath() => Path.Combine(Application.streamingAssetsPath, $"dialogue_{GetLanguage(_currentLanguage)}.json");

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

        if (_dialogues != null && _dialogues.Count > 0)
            CurrentDialogue = _dialogues[_startDialogueId];
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

    public void DisplayDialogueCoroutine()
    {
        StartCoroutine(DisplayDialogue());
    }

    public void DisplayDialogueWithChoiceCoroutine()
    {
        StartCoroutine(DisplayDialogueWithChoice());
    }

    public IEnumerator DisplayDialogue()
    {
        DialogueBox.SetActive(true);
        yield return StartCoroutine(DisplayText(CurrentDialogue.text));
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
            yield return new WaitForSeconds(DisplayLetterSpeed);
        }
    }

}
