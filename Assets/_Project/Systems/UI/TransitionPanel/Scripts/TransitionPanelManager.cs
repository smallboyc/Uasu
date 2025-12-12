using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class TransitionPanelManager : MonoBehaviour
{
    private UnityEngine.UI.Image _image;
    [SerializeField] private Color color;
    [SerializeField] private float _fadeDuration = 4.0f;
    private float _currentTime;
    void Awake()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        color.a = 1.0f;
        _image.color = color;
        _currentTime = 0.0f;
    }

    void Update()
    {
        if (_currentTime < _fadeDuration)
        {
            _currentTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1.0f - (_currentTime / _fadeDuration));
            _image.color = color;
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
