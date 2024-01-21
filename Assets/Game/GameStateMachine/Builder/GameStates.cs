using System;
using System.Collections.Generic;

public class GameStates
{
    private static GameStates _instance;
    private Dictionary<Type, BaseGameState> _states = new Dictionary<Type, BaseGameState>();

    public static GameStates Instance 
    { 
        get
        { 
            if(_instance == null)
                _instance = new GameStates();

            return _instance;
        }
    }

    public void EnterToLoadPool(IInitialized initialized)
    {
        LoaderGameState loader = (LoaderGameState)_states[typeof(LoaderGameState)];
        loader.Add(initialized);
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

    private GameStates()
    {
        _instance = this;
        _states.Add(typeof(FinishGameState), new FinishGameState());
        _states.Add(typeof(MainGameState), new MainGameState(_states[typeof(FinishGameState)]));
        _states.Add(typeof(LoaderGameState), new LoaderGameState(_states[typeof(MainGameState)]));
        _states.Add(typeof(StartGameState), new StartGameState(_states[typeof(LoaderGameState)]));
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