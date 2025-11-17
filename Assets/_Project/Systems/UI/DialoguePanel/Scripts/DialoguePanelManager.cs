using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [HideInInspector] public GameObject DialogueBox;
    [HideInInspector] public Image DialogueImage;
    [HideInInspector] public TMP_Text DialogueText;

    [Header("Choices UI")]
    [HideInInspector] public GameObject ChoiceBox;
    [HideInInspector] public List<Button> ChoiceButtons;

    private void Awake()
    {

        DialogueBox = transform.Find("DialogueBox").gameObject;
        if (DialogueBox != null)
        {
            DialogueImage = DialogueBox.transform.Find("DialogueImage")?.GetComponent<Image>();
            DialogueText = DialogueBox.transform.Find("DialogueText")?.GetComponent<TMP_Text>();
        }

        ChoiceBox = transform.Find("ChoiceBox").gameObject;
        if (ChoiceBox != null)
        {
            ChoiceButtons.Clear();
            foreach (Transform child in ChoiceBox.transform)
            {
                Button btn = child.GetComponent<Button>();
                if (btn != null)
                    ChoiceButtons.Add(btn);
            }
        }
        DialogueBox.SetActive(false);
        ChoiceBox.SetActive(false);
    }
}
