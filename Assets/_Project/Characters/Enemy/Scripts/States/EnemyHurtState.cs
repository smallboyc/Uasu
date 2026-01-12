using UnityEngine;

public class EnemyHurtState : EnemyState
{
    public EnemyHurtState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        // Debug.Log("ENEMY => Hurt State ENTER");
        // _enemyManager.HealthManager.StartHurt();
        if (SoundManager.Instance)
            SoundManager.Instance.PlaySoundClip(_enemyManager.HurtSound, _enemyManager.transform);
        _enemyManager.AnimationManager.PlayHurtAnimation();
    }

    public override void Update()
    {
        if (!_enemyManager.HealthManager.IsHurt)
        {
            if (_enemyManager.HealthManager.IsDead()) // Death :(
            {
                _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.DeathState);
            }
            else
            {
                _enemyManager.LockManager.HasLockedPlayer = true; // We force this to be true, cuz enemy becomes unaturally angry against the player.
                _enemyManager.EnemyStateMachine.ChangeState(_enemyManager.FocusState);
            }

        }
    }

    public override void Exit()
    {
        _enemyManager.AnimationManager.StopHurtAnimation();
        // Debug.Log("ENEMY => Hurt State EXIT");
    }
}
