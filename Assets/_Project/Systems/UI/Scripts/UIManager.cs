using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ERROR (UIManager): No Instance found.");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject _healthBar;


    private void Awake()
    {
        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (UIManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        _instance = this;
    }
}
