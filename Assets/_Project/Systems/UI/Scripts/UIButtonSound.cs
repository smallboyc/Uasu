using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour,
    IPointerEnterHandler,
    ISubmitHandler,
    ISelectHandler
{
    public enum UISoundType
    {
        Click,
        Hover,

    }

    [SerializeField] private UISoundType soundType;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.Instance == null)
            return;

        if (soundType == UISoundType.Hover)
            UIManager.Instance.PlayUIHover();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (UIManager.Instance == null)
            return;

        if (soundType == UISoundType.Click)
            UIManager.Instance.PlayUIClick();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (UIManager.Instance == null)
            return;

        UIManager.Instance.PlayUINavigation();
    }
}