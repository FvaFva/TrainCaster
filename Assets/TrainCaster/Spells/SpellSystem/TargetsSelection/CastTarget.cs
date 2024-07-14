﻿using System.Collections.Generic;
using UnityEngine;

public struct CastTarget
{
    private List<EnemyRouter> _enemies;

    public CastTarget(GameObject target, bool isCorrect)
    {
        _enemies = new List<EnemyRouter>();

        if (target.TryGetComponent<EnemyRouter>(out EnemyRouter temp))
        {
            Enemy = temp;
            _enemies.Add(Enemy);
            Point = Enemy.Position;
        }
        else
        {
            Point = target.transform.position;
            Enemy = null;
        }

        IsCorrect = isCorrect;
    }

    public CastTarget(Vector3 point, bool isCorrect)
    {
        _enemies = new List<EnemyRouter>();
        Point = point;
        Enemy = null;
        IsCorrect = isCorrect;
    }

    public CastTarget(EnemyRouter enemy, bool isCorrect)
    {
        _enemies = new List<EnemyRouter>();
        Point = enemy.Position;
        Enemy = enemy;
        IsCorrect = isCorrect;
    }

    public void ApplyEnemy(EnemyRouter enemy)
    {
        if(_enemies.Contains(enemy))
            return;

        _enemies.Add(enemy);
    }

    public Vector3 Point {  get; private set; }
    public EnemyRouter Enemy { get; private set; }
    public bool IsCorrect { get; private set; }
    public IEnumerable<EnemyRouter> Enemies => _enemies;
}