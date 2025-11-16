using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelManager : MonoBehaviour
{
    static DialoguePanelManager _instance;
    public static DialoguePanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ERROR (DialoguePanelManager): No Instance found.");
            }
            return _instance;
        }
    }
    [Header("Dialogue UI")]
    public GameObject DialoguePanel;
    public GameObject DialogueBox;
    public Image DialogueImage;
    public TMP_Text DialogueText;

    [Header("Choices UI")]
    public GameObject ChoiceBox;
    public List<Button> ChoiceButtons;

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
    }
}
