using System.Collections;
using System.Collections.Generic;
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
            Point = Enemy.transform.position;
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
        _enemies = new List<EnemyRouter>() { enemy };
        Enemy = enemy;
        Point = Enemy.transform.position;
        IsCorrect = isCorrect;
    }

    public Vector3 Point {  get; private set; }
    public EnemyRouter Enemy { get; private set; }
    public bool IsCorrect { get; private set; }
    public IEnumerable Enemies => _enemies;
}