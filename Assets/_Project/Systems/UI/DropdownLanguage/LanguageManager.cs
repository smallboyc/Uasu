using System.IO;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    void Start()
    {
        LoadDialogues(0);
    }
    
    private string GetLanguage(int id) => id == 0 ? "en" : "es";
    private string GetDialoguePath(int id) => Path.Combine(Application.streamingAssetsPath, $"dialogue_{GetLanguage(id)}.json");
    public void LoadDialogues(int id)
    {
        string path = GetDialoguePath(id);

        if (File.Exists(path))
        {
            string jsonDialogue = File.ReadAllText(path);
            DialogueManager.Instance.Dialogues = JsonUtility.FromJson<DialogueData>(jsonDialogue).dialogues;
        }
        else
        {
            Debug.LogError($"Cannot find language file at: {path}");
        }
    }
}
