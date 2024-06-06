using System;

public interface IGameTransit 
{
    public event Action<BaseGameState> Finished;
}