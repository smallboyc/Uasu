using UnityEngine;

public abstract class SorcererState : State
{
    protected SorcererManager _sorcererManager;

    public SorcererState(SorcererManager sorcererManager)
    {
        _sorcererManager = sorcererManager;
    }
}
