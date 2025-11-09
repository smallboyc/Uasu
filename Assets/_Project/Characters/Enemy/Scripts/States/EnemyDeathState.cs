using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        Debug.Log("ENEMY : Death State (Enter)");
        _enemyManager.IsDead = true;
        _enemyManager.AnimationManager.PlayDeathAnimation();

        if (_enemyManager.CharacterController)
            _enemyManager.CharacterController.enabled = false;
    }

    public override void Update()
    {
        //TODO : Instantiate a Soul to give to the player.
    }

    public override void Exit()
    {
    }
}
