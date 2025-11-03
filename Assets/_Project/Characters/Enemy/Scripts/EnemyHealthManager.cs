using UnityEngine;

public class EnemyHealthManager : CharacterHealthManager
{
    public override void Feal()
    {
        base.Feal();
    }

    public override void Hit(float stunnedCooldown = 0.2F)
    {
        base.Hit(stunnedCooldown);
        // _health--;
    }

    public void GiveSoul()
    {
        Debug.Log("Give a Soul !");
    }

}
