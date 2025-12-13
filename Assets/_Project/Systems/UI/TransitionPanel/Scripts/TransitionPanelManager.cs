using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class TransitionPanelManager : MonoBehaviour
{
    private enum TransitionType { FadeIn, FadeOut };
    private TransitionType _currentTransition = TransitionType.FadeIn;
    private UnityEngine.UI.Image _image;
    private Color color;
    [SerializeField] private float _fadeDuration = 4.0f;
    private float _currentTime;
    void Awake()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
    }

    private void OnEnable()
    {
        _currentTime = 0.0f;
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            _currentTransition = TransitionType.FadeOut;
            color.a = 0.0f;
            color = Color.black;
            _image.color = color;
        }
        else
        {
            _currentTransition = TransitionType.FadeIn;
            color.a = 1.0f;
            color = Color.white;
            _image.color = color;
        }
    }

    void Update()
    {
        if (_currentTime < _fadeDuration)
        {
            _currentTime += Time.deltaTime;
            if (_currentTransition == TransitionType.FadeIn)
                color.a = Mathf.Clamp01(1.0f - (_currentTime / _fadeDuration));
            else
                color.a = Mathf.Clamp01(_currentTime / _fadeDuration);
            _image.color = color;
        }
    }
}
