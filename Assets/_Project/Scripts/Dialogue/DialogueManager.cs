using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DialogueManager : MonoBehaviour
{
    public enum Language { EN, FR }
    public Language currentLanguage = Language.EN;

    private Dictionary<string, string> _dialogue = new();

    private string GetLanguage(Language language) => language == Language.EN ? "en" : "fr";
    private string GetDialoguePath() => Path.Combine(Application.streamingAssetsPath, $"dialogue_{GetLanguage(currentLanguage)}.json");

    public void LoadLanguage()
    {
        string path = GetDialoguePath();

        if (File.Exists(path))
        {
            string jsonDialogue = File.ReadAllText(path);
            _dialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonDialogue);
        }
        else
        {
            Debug.LogError($"Cannot find language file at : {path}");
        }
    }

    void Start()
    {
        LoadLanguage();

        if (_dialogue != null && _dialogue.Count > 0)
        {
            foreach (KeyValuePair<string, string> kvp in _dialogue)
            {
                Debug.Log($"{kvp.Key} = {kvp.Value}");
            }
        }
        else
        {
            Debug.LogWarning("Dialogue dictionary is null or empty!");
        }
    }
}

