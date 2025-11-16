using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBarManager : MonoBehaviour
{
    static PlayerHealthBarManager _instance;
    public static PlayerHealthBarManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ERROR (PlayerHealthBarManager): No Instance found.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        //Singleton
        if (_instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (PlayerHealthBarManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        _instance = this;
    }

    public Slider slider;
    public Gradient gradient;
    public Image fill;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }


}
