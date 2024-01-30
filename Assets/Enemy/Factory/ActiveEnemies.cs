using System;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemies
{
    private List<EnemyRouter> _enemies;
    private int _count;

    public ActiveEnemies(EnemyFactory enemyFactory)
    {
        enemyFactory.Created += OnCreate;
        _enemies = new List<EnemyRouter>();
    }

    public event Action<EnemyRouter, EnemyDeleteStatus> Deleted;

    public IEnumerable<EnemyRouter> AllEnemies => _enemies;

    public EnemyRouter GetRandom()
    {
        if (_count == 0)
            return null;

        return _enemies[UnityEngine.Random.Range(0, _count)];
    }

    public EnemyRouter GetNearest(Vector3 point)
    {
        if (_count == 0)
            return null;

        float nearDelta = float.MaxValue;
        int nearest = -1;

        for(int i = 0;  i < _count; i++)
        {
            float currentDelta = (point - _enemies[i].Position).sqrMagnitude;

            if (currentDelta < nearDelta)
            {
                nearDelta = currentDelta;
                nearest = i;
            }
        }

        return _enemies[nearest];
    }

    public List<EnemyRouter> GetAllInRadius(Vector3 point, float radius)
    {
        if (_count == 0 || radius < 0)
            return null;

        List<EnemyRouter> list = new List<EnemyRouter>();

        foreach (EnemyRouter enemy in _enemies)
        {
            if(Vector3.Distance(enemy.Position, point) <= radius)
                list.Add(enemy);
        }

        return list;
    }

    private void OnDeleted(EnemyRouter enemy, EnemyDeleteStatus reason)
    {
        _enemies.Remove(enemy);
        _count--;
        enemy.Deleted -= OnDeleted;
        Deleted?.Invoke(enemy, reason);
    }

    private void OnCreate(EnemyRouter enemy)
    {
        _enemies.Add(enemy);
        _count++;
        enemy.Deleted += OnDeleted;
    }
}
