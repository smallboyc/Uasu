using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class TransitionPanelManager : MonoBehaviour
{
    private UnityEngine.UI.Image _image;
    [SerializeField] private float _fadeDuration = 4.0f;
    void Awake()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // _image.color = Color.white;
        // _image.color.a
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 5.0f)
        {
            _image.color = Color.red;
        }

    }
}
