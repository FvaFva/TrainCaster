using System;
using System.Collections.Generic;

public class GameState
{
    private Dictionary<Type, BaseGameState> _states = new Dictionary<Type, BaseGameState>();
    private LoaderGameState _loader;

    public GameState() 
    {
        _states.Add(typeof(FinishGameState), new FinishGameState());
        _states.Add(typeof(MainGameState), new MainGameState(_states[typeof(FinishGameState)]));
        _loader = new LoaderGameState(_states[typeof(MainGameState)]);
        _states.Add(typeof(LoaderGameState), _loader);
        _states.Add(typeof(StartGameState), new StartGameState(_states[typeof(LoaderGameState)]));
    }

    public void EnterToLoadPool(IInitialized initialized)
    {
        _loader.Add(initialized);
    }

    public BaseGameState GetState<StateType>() where StateType : BaseGameState
    {
        HaveState<StateType>(out BaseGameState temp);
        return temp;
    }

    public void AddTransitToState<StateType>(IGameTransit transit) where StateType : BaseGameState
    {
        if (HaveState<StateType>(out BaseGameState temp))
            temp.ApplyTransit(transit);
    }

    private bool HaveState<StateType>(out BaseGameState state) where StateType : BaseGameState
    {
        Type stateType = typeof(StateType);
        bool haveState = _states.ContainsKey(stateType);

        if (haveState)
            state = _states[stateType];
        else
            state = null;

        return haveState;
    }
}