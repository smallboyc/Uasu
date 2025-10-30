using UnityEngine;

public class PlayerHealthManager : CharacterHealthManager
{
    public override void Feal()
    {
        base.Feal();
    }

    public override void Hit(float stunnedCooldown = 0.2F)
    {
        base.Hit(stunnedCooldown);
    }
}
