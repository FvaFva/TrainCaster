using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour, IInitialized
{
    [SerializeField] private List<int> _countPreload;
    [SerializeField] private List<EnemyModel> _models;
    [SerializeField] private EnemyPath _path;
    [SerializeField] private EnemyRouter _enemy;

    private List<EnemyRouter> _enemies = new List<EnemyRouter>();

    public event Action EnemyFinishPath;

    private void OnEnable()
    {
        foreach (EnemyRouter enemy in _enemies)
        {
            enemy.PathFinished += OnEnemyFinish;
        }
    }

    private void OnDisable()
    {
        foreach (EnemyRouter enemy in _enemies)
        {
            enemy.PathFinished -= OnEnemyFinish;
        }
    }

    private void Awake()
    { 
        GameStates.Instance.EnterToLoadPool(this);
    }

    public void Init()
    {
        int minLength = Mathf.Min(_countPreload.Count, _models.Count);

        for (int i = 0; i < minLength; i++)
        {
            Storage.Instance.Put(_models[i].gameObject, _countPreload[i]);
        }
    }

    public void CreateEnemy()
    {
        if (Storage.Instance.TryGetFree<EnemyModel>(_models[0].gameObject, out EnemyModel model))
        {
            EnemyRouter enemy = Instantiate(_enemy, transform.position, default, transform);
            enemy.gameObject.name = _models[0].name;
            enemy.InitModel(model);
            enemy.StartPath(_path);
            enemy.PathFinished += OnEnemyFinish;
            _enemies.Add(enemy);
        }
    }

    private void OnEnemyFinish()
    {
        EnemyFinishPath?.Invoke();
    }
}
