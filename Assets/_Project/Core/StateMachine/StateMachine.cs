
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState)
    {
        if (newState.Priority >= CurrentState.Priority)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        else
        {
            Debug.Log("Check Priority.");
        }
    }
}