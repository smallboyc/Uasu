using UnityEngine;

public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        Debug.Log("ENEMY : Death State (Enter)");
        _enemyManager.AnimationManager.PlayDeathAnimation();
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(_enemyManager.DeathSound, _enemyManager.transform);

        if (_enemyManager.CharacterController)
            _enemyManager.CharacterController.enabled = false;

        _enemyManager.HealthManager.GiveSoul();
    }

    public override void Update()
    {
        //TODO : Instantiate a Soul to give to the player.
    }

    public override void Exit()
    {
    }
}
