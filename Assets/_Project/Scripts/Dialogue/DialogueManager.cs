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
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private Image _dialogueImage;
    [SerializeField] private TMP_Text _dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject _choiceBox;
    [SerializeField] private List<Button> _choiceButtons;

    [Header("Settings")]
    [SerializeField] private float _displayLetterSpeed = 0.02f;

    private List<DialogueEntry> _dialogues = new();
    private DialogueEntry _currentDialogue;

    private bool _canInteract = true;
    private int _startDialogueId = 0;

    private void Awake()
    {
        _dialoguePanel.SetActive(false);
        _dialogueBox.SetActive(false);
        _choiceBox.SetActive(false);

        LoadDialogues();
    }

    private void Start()
    {
        if (_dialogues != null && _dialogues.Count > 0)
            _currentDialogue = _dialogues[_startDialogueId];
    }

    //
    private void Update()
    {
        if (PlayerInputManager.Instance.InteractPressed && _canInteract)
        {
            _dialogueText.text = "";
            _dialoguePanel.SetActive(true);

            if (DialogueHasChoices())
                StartCoroutine(DisplayDialogueWithChoice());
            else
                StartCoroutine(DisplayDialogue());
        }
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
    }

    private bool DialogueHasChoices() => _currentDialogue.choices != null && _currentDialogue.choices.Count > 0;



    private IEnumerator DisplayDialogue()
    {
        _dialogueBox.SetActive(true);
        _choiceBox.SetActive(false);
        _canInteract = false;

        yield return StartCoroutine(DisplayText(_currentDialogue.text));

        // Wait for player to interact before :
        // 1 - close the last dialogue
        // 2 - Display the next dialogue
        yield return new WaitUntil(() => PlayerInputManager.Instance.InteractPressed);

        if (_currentDialogue.is_end)
        {
            StartCoroutine(EndDialogue());
            yield break;
        }

        if (_currentDialogue.next_dialogue >= 0)
        {
            _currentDialogue = _dialogues.Find(d => d.id == _currentDialogue.next_dialogue);

            if (DialogueHasChoices())
                StartCoroutine(DisplayDialogueWithChoice());
            else
                StartCoroutine(DisplayDialogue());
        }
    }


    private IEnumerator DisplayDialogueWithChoice()
    {
        _dialogueBox.SetActive(true);
        _choiceBox.SetActive(false);
        _canInteract = false;

        yield return StartCoroutine(DisplayText(_currentDialogue.text));

        DisplayChoices();
        _canInteract = true;
    }

    private IEnumerator DisplayText(string text)
    {
        _dialogueText.text = "";
        foreach (char letter in text)
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_displayLetterSpeed);
        }
    }


    private void DisplayChoices()
    {
        _choiceBox.SetActive(true);

        foreach (var btn in _choiceButtons)
            btn.gameObject.SetActive(false);

        for (int i = 0; i < _currentDialogue.choices.Count && i < _choiceButtons.Count; i++)
        {
            var button = _choiceButtons[i];
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<TMP_Text>().text = _currentDialogue.choices[i].text;

            int index = i; // we need to keep that value (because "i" passed directly to the listener is a ref)
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        _canInteract = false;
        _choiceBox.SetActive(false);

        int nextId = _currentDialogue.choices[choiceIndex].next_dialogue;
        _currentDialogue = _dialogues.Find(d => d.id == nextId);

        if (DialogueHasChoices())
            StartCoroutine(DisplayDialogueWithChoice());
        else
            StartCoroutine(DisplayDialogue());
    }

    private IEnumerator EndDialogue()
    {
        _dialoguePanel.SetActive(false);
        _dialogueBox.SetActive(false);
        _choiceBox.SetActive(false);
        _dialogueText.text = "";
        yield return new WaitForSeconds(0.2f);
        _canInteract = true;
    }
}
