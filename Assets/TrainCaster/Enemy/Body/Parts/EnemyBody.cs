using System;
using UnityEngine;

public class EnemyBody : MonoBehaviour, IEnemyPart
{
    private const float ArmorUnitImpact = 0.97f;

    [SerializeField] private ProgressBar _hitPointsView;

    private HitPoints _hitPoints;
    private float _armorBase;
    private float _armorChanging;

    public event Action<EnemyDeleteStatus> Completed;

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
        _armorBase = model.Armor;
        _armorChanging = 0;
    }

    public void ImplementStatus(EnemyStatusParameters status)
    {
        _armorChanging = status.Armor;
    }

    public void ApplyDamage(float damage)
    {
        _hitPoints.ApplyDamage(damage * Mathf.Pow(ArmorUnitImpact - _armorChanging, _armorBase));
    }

    private void OnDie()
    {
        Completed?.Invoke(EnemyDeleteStatus.Die);
    }
}
