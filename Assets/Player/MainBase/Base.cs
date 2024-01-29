using System;
using UnityEngine;
using Zenject;

public class Base : MonoBehaviour
{
    [SerializeField] private float _hitPointsMax;
    [SerializeField] private ProgressBar _hitPointsBar;

    [Inject] private ActiveEnemies _enemies;

    private HitPoints _hitPoints = new HitPoints(0,0);

    public event Action Destroyed;

    private void Awake()
    {
        _hitPoints = new HitPoints(_hitPointsMax, 0);
        _hitPointsBar.SetSource(_hitPoints);
        _hitPoints.Die += OnDie;
    }

    private void OnDisable()
    {
        _enemies.Deleted -= OnEnemyDeleted;
        _hitPoints.Die -= OnDie;
    }

    private void OnEnable()
    {
        _enemies.Deleted += OnEnemyDeleted;
        _hitPoints.Die += OnDie;
    }

    private void OnEnemyDeleted(EnemyRouter enemy, EnemyDeleteStatus status)
    {
        if(status == EnemyDeleteStatus.FinishPath)
            _hitPoints.ApplyDamage(1);
    }

    private void OnDie()
    {
        Destroyed?.Invoke();
    }
}
