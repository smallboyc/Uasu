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

    //!! => equivalent StartHurt() is the player setting IsHurt to true from the PlayerAttackManager.
    
    //Use as an Event in the Hurt Animation
    public void EndHurt()
    {
        Debug.Log("ANIM = Trigger End Hurt");
        _isHurt = false;
    }
}
