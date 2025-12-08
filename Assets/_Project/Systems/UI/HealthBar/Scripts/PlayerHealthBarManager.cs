using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
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

    private Slider _slider;
    public Gradient gradient;
    public Image fill;




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

        _slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        _slider.maxValue = health;
        SetHealth(health);
    }
    public void SetHealth(int health)
    {
        _slider.value = health;
        fill.color = gradient.Evaluate(_slider.normalizedValue);
    }


    public float GetMaxHealth() => _slider.maxValue;

}
