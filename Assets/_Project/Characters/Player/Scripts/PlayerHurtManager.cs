using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHurtManager : MonoBehaviour
{
    private bool _isHurt = false;
    public bool IsHurt
    {
        get => _isHurt;
        set => _isHurt = value;
    }

    //!! => equivalent StartHurt() is the player setting IsHurt to true from the EnemyAttackManager.

    //Use as an Event in the Hurt Animation
    public void EndHurt()
    {
        // Debug.Log("ANIM = Trigger End Hurt");
        _isHurt = false;
    }
}
