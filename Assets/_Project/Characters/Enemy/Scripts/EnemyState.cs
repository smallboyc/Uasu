public abstract class EnemyState : State
{
    protected EnemyManager _enemyManager;
   
    public EnemyState(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
    }
}