using System;
using System.Collections.Generic;

public class ActiveEnemies
{
    private List<EnemyRouter> _enemies;

    public ActiveEnemies(EnemyFactory enemyFactory)
    {
        enemyFactory.Created += OnEnemyCreate;
        _enemies = new List<EnemyRouter>();
    }

    public IEnumerable<EnemyRouter> Enemies => _enemies;

    public event Action<EnemyRouter, EnemyDeleteStatus> Deleted;

    private void OnEnemyDeleted(EnemyRouter enemy, EnemyDeleteStatus reason)
    {
        _enemies.Remove(enemy);
        enemy.Deleted -= OnEnemyDeleted;
        Deleted?.Invoke(enemy, reason);
    }

    private void OnEnemyCreate(EnemyRouter enemy)
    {
        _enemies.Add(enemy);
        enemy.Deleted += OnEnemyDeleted;
    }
}
