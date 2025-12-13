using UnityEngine;
using UnityEngine.UI;

public class KeyItemUIIcon : MonoBehaviour
{
    [SerializeField] private Image KeyItemSlot_Sword;
   

    private void Awake()
    {
        KeyItemSlot_Sword.enabled = false; // Oculto al iniciar
    }

    public void ShowSwordIcon()
    {
        KeyItemSlot_Sword.enabled = true;
    }

    //public void HideSwordIcon()
    //{
   //     KeyItemSlot_Sword.enabled = false;
   // }
}
