using System.Collections;
using UnityEngine;

public class EnemyHurtManager : MonoBehaviour
{
    private bool _isHurt = false;
    public bool IsHurt
    {
        get => _isHurt;
        set => _isHurt = value;
    }

    //Start on Enter.
    // public void StartHurt()
    // {
    //     Debug.Log("ANIM = Trigger Start Hurt");
    //     _isHurt = true;
    // }

    //Use as an Event in the Hurt Animation
    public void EndHurt()
    {
        Debug.Log("ANIM = Trigger End Hurt");
        _isHurt = false;
    }
}
