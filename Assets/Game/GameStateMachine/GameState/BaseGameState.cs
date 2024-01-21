using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseGameState 
{
    readonly private List<IGameTransit> _transits = new List<IGameTransit>();

    public event Action<BaseGameState> NewStateFound;

    private BaseGameState _stateOnEndAction;
    private bool _isActive = false;

    public BaseGameState(BaseGameState stateOnEndAction, IEnumerable < IGameTransit > transits = null)
    {
        _stateOnEndAction = stateOnEndAction;
        
        if(transits != null)
        {
            foreach(IGameTransit transit in transits)
                _transits.Add(transit);
        }
    }

    public void Enter()
    {
        _isActive = true;

        foreach (IGameTransit transit in _transits)
            transit.Finished += NextStateFound;
    }

    public void Exit()
    {
        _isActive = false;

        foreach (IGameTransit transit in _transits)
            transit.Finished -= NextStateFound;
    }

    public void ApplyTransit(IGameTransit transit)
    {
        if (_isActive)
            transit.Finished += NextStateFound;

        _transits.Add(transit);
    }

    public IEnumerator Action()
    {
        yield return null;

        foreach (var ActionStep in ActionSpecific())
            yield return ActionStep;

        NewStateFound?.Invoke(_stateOnEndAction);
    }

    protected abstract IEnumerable ActionSpecific();

    private void NextStateFound(BaseGameState nexState)
    {
        NewStateFound?.Invoke(nexState);
    }
}