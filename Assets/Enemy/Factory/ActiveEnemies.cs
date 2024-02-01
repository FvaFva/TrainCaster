using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public EnemyRouter GetRandom(IEnumerable<EnemyRouter> exception = null)
    {
        if (Check(exception))
            return null;

        int random = UnityEngine.Random.Range(0, _count);

        if (exception != null)
            return _enemies.Except(exception).OrderBy(x => random).First();
        else
            return _enemies[random];
    }

    public EnemyRouter GetNearest(Vector3 point, IEnumerable<EnemyRouter> exception = null)
    {
        if(Check(exception)) 
            return null;

        float nearDelta = float.MaxValue;
        int nearest = -1;
        bool haveException = exception != null;

        for(int i = 0;  i < _count; i++)
        {
            if (haveException && exception.Contains(_enemies[i]))
                continue;

            float currentDelta = (point - _enemies[i].Position).sqrMagnitude;

            if (currentDelta < nearDelta)
            {
                nearDelta = currentDelta;
                nearest = i;
            }
        }

        return _enemies[nearest];
    }

    public List<EnemyRouter> GetAllInRadius(Vector3 point, float radius, IEnumerable<EnemyRouter> exception = null)
    {
        if (radius <= 0 || Check(exception))
            return null;

        List<EnemyRouter> list = new List<EnemyRouter>();
        bool haveException = exception != null;

        foreach (EnemyRouter enemy in _enemies)
        {
            if (haveException && exception.Contains(enemy))
                continue;

            if (Vector3.Distance(enemy.Position, point) <= radius)
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

    private bool Check(IEnumerable<EnemyRouter> exception)
    {
        if (_count == 0)
            return true;

        if (exception != null && new HashSet<EnemyRouter>(_enemies).SetEquals(exception))
            return true;

        return false;
    }

    private void OnCreate(EnemyRouter enemy)
    {
        _enemies.Add(enemy);
        _count++;
        enemy.Deleted += OnDeleted;
    }
}
