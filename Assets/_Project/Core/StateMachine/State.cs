
public abstract class State
{
    public int Priority { get; protected set; }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}