using System;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IEnemyPart
{
    [SerializeField] private ProgressBar _hitPointsView;
    private HitPoints _hitPoints;

    private void Awake()
    {
        _hitPoints = new HitPoints(0, 0);
        _hitPointsView.SetSource(_hitPoints);
    }

    private void OnEnable()
    {
        _hitPoints.Die += TakeOff;
    }

    private void OnDisable()
    {
        _hitPoints.Die -= TakeOff;
    }

    public event Action TakeOff;

    public void ImplementModel(EnemyModel model)
    {
        _hitPoints.SetNew(model.GetRandomizeHitPoints());
    }

    public void ApplyDamage(float damage)
    {
        _hitPoints.ApplyDamage(damage);
    }
}
