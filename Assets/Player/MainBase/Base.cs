using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<EnemyFactory> _factories = new List<EnemyFactory>();
    [SerializeField] private float _hitPointsMax;
    [SerializeField] private ProgressBar _hitPointsBar;

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
        foreach (EnemyFactory factory in _factories)
        {
            factory.EnemyFinishPath -= ApplyDamage;
        }

        _hitPoints.Die -= OnDie;
    }

    private void OnEnable()
    {
        foreach (EnemyFactory factory in _factories)
        {
            factory.EnemyFinishPath += ApplyDamage;
        }

        _hitPoints.Die += OnDie;
    }

    private void ApplyDamage()
    {
        _hitPoints.ApplyDamage(1);
    }

    private void OnDie()
    {
        Destroyed?.Invoke();
    }
}
