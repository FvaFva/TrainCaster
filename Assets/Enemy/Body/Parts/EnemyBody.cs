using System;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IEnemyPart
{
    [SerializeField] private ProgressBar _hitPointsView;
    private HitPoints _hitPoints;

    public event Action<bool> Finished;

    private void Awake()
    {
        _hitPoints = new HitPoints(1, 0);
        _hitPointsView.SetSource(_hitPoints);
    }

    private void OnEnable()
    {
        _hitPoints.Die += OnDie;
    }

    private void OnDisable()
    {
        _hitPoints.Die -= OnDie;
    }


    public void ImplementModel(EnemyView model)
    {
        _hitPoints.SetNew(model.GetRandomizeHitPoints());
    }

    public void ApplyDamage(float damage)
    {
        _hitPoints.ApplyDamage(damage);
    }

    private void OnDie()
    {
        Finished?.Invoke(true);
    }
}
