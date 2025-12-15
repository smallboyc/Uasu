using UnityEngine;

public class PersistentEventSystem : MonoBehaviour
{
    public static PersistentEventSystem Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}