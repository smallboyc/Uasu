using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHealthManager : CharacterHealthManager
{

    public override void Feal()
    {
        base.Feal();
    }

    public override void Hit(float stunnedCooldown = 0.2F)
    {
        //Stunn
        base.Hit(stunnedCooldown);
        
    }
}
