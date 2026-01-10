using TMPro;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public static HelpManager Instance;

    [SerializeField] private TextMeshProUGUI _helpText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetHelpText(string text)
    {
        if (_helpText)
            _helpText.text = text;
    }
}
