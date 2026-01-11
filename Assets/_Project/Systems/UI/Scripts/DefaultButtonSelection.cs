using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButtonSelection : MonoBehaviour
{
    public GameObject firstSelectedButton;

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}