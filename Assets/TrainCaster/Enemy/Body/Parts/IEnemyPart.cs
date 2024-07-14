﻿using System;

public interface IEnemyPart
{
    public event Action<EnemyDeleteStatus> Completed;
    public void ImplementModel(EnemyView model);
    public void ImplementStatus(EnemyStatusParameters status);
}