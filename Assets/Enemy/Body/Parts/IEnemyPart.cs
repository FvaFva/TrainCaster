using System;

public interface IEnemyPart
{
    public event Action<bool> Finished;
    public void ImplementModel(EnemyView model);
}