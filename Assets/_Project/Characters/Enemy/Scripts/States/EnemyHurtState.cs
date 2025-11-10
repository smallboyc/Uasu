using UnityEngine;

public class EnemyHurtState : EnemyState
{
    public EnemyHurtState(EnemyManager enemyManager) : base(enemyManager) { }
    public override void Enter()
    {
        // Debug.Log("ENEMY => Hurt State ENTER");
        _enemyManager.Health--;
        Debug.Log($"Enemy health = {_enemyManager.Health}");
        // _enemyManager.HurtManager.StartHurt();
        _enemyManager.AnimationManager.PlayHurtAnimation();
    }

    public override void Update()
    {
        if (!_enemyManager.HurtManager.IsHurt)
        {
            if (_enemyManager.Health <= 0) // Death :(
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
