using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour,
    IPointerEnterHandler,
    IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.Instance == null)
            return;

        switch (soundType)
        {
            case UISoundType.Click:
                UIManager.Instance.PlayUIClick();
                break;
           
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (UIManager.Instance == null)
            return;

        UIManager.Instance.PlayUINavigation();
    }
}