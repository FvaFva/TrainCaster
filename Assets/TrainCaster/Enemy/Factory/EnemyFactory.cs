using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour, IInitialized
{
    [SerializeField] private List<int> _countPreload;
    [SerializeField] private List<EnemyView> _models;
    [SerializeField] private EnemyPath _path;
    [SerializeField] private EnemyRouter _enemy;
    [SerializeField] private int _testCountPreload;

    [Inject] private PoolService _poolService;
    [Inject] private GameStateBuilder _game;

    public event Action<EnemyRouter> Created;

    private void Awake()
    {
        _game.EnterToLoadPool(this);
    }

    public void Init()
    {
        int minLength = Mathf.Min(_countPreload.Count, _models.Count);
        _poolService.Put(_enemy, transform, _testCountPreload);

        for (int i = 0; i < minLength; i++)
        {
            _poolService.Put(_models[i], transform, _countPreload[i], _models[i].name);
        }
    }

    public void CreateEnemy()
    {
        EnemyView model = _poolService.Resolve<EnemyView>(_models[0].name);
        EnemyRouter enemy = _poolService.Resolve<EnemyRouter>();
        enemy.Activate(model);
        enemy.StartPath(_path);
        Created?.Invoke(enemy);
    }
}
