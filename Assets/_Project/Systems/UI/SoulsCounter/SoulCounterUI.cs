using TMPro;
using UnityEngine;

public class SoulCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI soulText;

    void Update()
    {
        if (PlayerManager.Instance != null)
        {
            soulText.text = PlayerManager.Instance.SoulCounter.ToString();
        }
    }
}