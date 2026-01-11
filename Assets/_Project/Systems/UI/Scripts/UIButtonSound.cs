using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour,
    IPointerEnterHandler,
    IPointerClickHandler,
    ISubmitHandler,
    ISelectHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager.Instance == null)
            return;
        UIManager.Instance.PlayUIHover();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (UIManager.Instance == null)
            return;
        UIManager.Instance.PlayUIClick();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.Instance == null)
            return;


        UIManager.Instance.PlayUIClick();



    }

    public void OnSelect(BaseEventData eventData)
    {
        if (UIManager.Instance == null)
            return;

        UIManager.Instance.PlayUINavigation();
    }
}