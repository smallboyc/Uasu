using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHealthManager : MonoBehaviour
{
    public int Health = 4;
    public bool IsHurt;

    public bool IsDead()
    {
        return Health <= 0;
    }
    public void Hurt()
    {
        IsHurt = true;
        Health--;
        PlayerHealthBarManager.Instance.SetHealth(Health);
    }

    //Used as an Event in the Hurt Animation
    public void EndHurt()
    {
        // Debug.Log("ANIM = Trigger End Hurt");
        IsHurt = false;
    }

}
