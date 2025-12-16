using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionPanelManager : MonoBehaviour
{
    public static TransitionPanelManager Instance;
    public bool IsActiveTransition;
    public enum TransitionType { FadeIn, FadeOut }; //In => 1 -> 0 / Out => 0 -> 1
    private TransitionType _currentTransition;
    public enum TransitionColor
    {
        White, Black
    };
    [SerializeField] private UnityEngine.UI.Image _image;
    private Color color;
    [SerializeField] private float _fadeDuration = 4.0f;
    private float _currentTime;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log($"ERROR (TransitionPanelManager): ({gameObject.name}) GameObject has been deleted because of the Singleton Pattern");
            return;
        }
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void NewTransition(TransitionType transitionType, TransitionColor transitionColor)
    {
        IsActiveTransition = true;
        _currentTransition = transitionType;
        color.a = _currentTransition == TransitionType.FadeOut ? 0.0f : 1.0f;
        color = transitionColor == TransitionColor.White ? Color.white : Color.black;
        _image.color = color;

        UIManager.Instance.Show(PanelType.Transition);
        _currentTime = 0.0f;
    }

    private void FadeIn()
    {
        if (_currentTime < _fadeDuration)
        {
            _currentTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1.0f - (_currentTime / _fadeDuration));
            _image.color = color;
        }
        else
        {
            IsActiveTransition = false;
            UIManager.Instance.Hide(PanelType.Transition);
        }
    }

    private void FadeOut()
    {
        if (_currentTime < _fadeDuration)
        {
            _currentTime += Time.deltaTime;
            color.a = Mathf.Clamp01(_currentTime / _fadeDuration);
            _image.color = color;
        }
        else
        {
            IsActiveTransition = false;
        }
    }

    void Update()
    {
        if (IsActiveTransition)
        {
            if (_currentTransition == TransitionType.FadeIn)
                FadeIn(); //In => 1 -> 0
            else
                FadeOut(); // Out => 0 -> 1
        }
        // if (_currentTime < _fadeDuration)
        // {
        //     _currentTime += Time.deltaTime;
        //     if (_currentTransitionType == TransitionType.FadeIn)
        //         color.a = Mathf.Clamp01(1.0f - (_currentTime / _fadeDuration));
        //     else
        //         color.a = Mathf.Clamp01(_currentTime / _fadeDuration);
        //     _image.color = color;
        // }
        // else if (_currentTransitionType == TransitionType.FadeIn)
        // {
        //     UIManager.Instance.Hide(PanelType.Transition);
        // }
    }
}
