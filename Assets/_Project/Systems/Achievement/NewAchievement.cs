using UnityEngine;

public class NewAchievement : MonoBehaviour
{
    [SerializeField] private string _achievementName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerManager.Instance)
                PlayerManager.Instance.AddAchievement(_achievementName);
        }
    }
}
