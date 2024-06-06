using UnityEngine;

public class EnemyCurrencyMiner : CurrencyMiner
{
    [SerializeField] private EnemyRouter _enemy;

    protected override void DisableImpact()
    {
        _enemy.ViewChanged -= ConnectModel;
        _enemy.Deleted -= OnDeletedEnemy;
    }

    protected override void EnableImpact()
    {
        _enemy.ViewChanged += ConnectModel;
        _enemy.Deleted += OnDeletedEnemy;
    }
    private void ConnectModel(EnemyView model)
    {
        UpdateMineValues(model.RewardBasic, model.RewardDice);
    }

    private void OnDeletedEnemy(EnemyRouter router, EnemyDeleteStatus status)
    {
        if (status == EnemyDeleteStatus.Die)
            TriggerMine(_enemy.Position);
    }
}