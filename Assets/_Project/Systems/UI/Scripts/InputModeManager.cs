using UnityEngine;
using UnityEngine.EventSystems;

public class InputModeManager : MonoBehaviour
{
    private bool isMouseInput = false;

    void Update()
    {
        if (Input.mousePresent && Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if (!isMouseInput)
            {
                isMouseInput = true;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        else
        {
            if (isMouseInput)
            {
                isMouseInput = false;
            }
        }
    }
}
