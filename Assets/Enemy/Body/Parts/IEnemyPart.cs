using System;

public interface IEnemyPart
{
    public event Action TakeOff;
    public void ImplementModel(EnemyModel model);
}