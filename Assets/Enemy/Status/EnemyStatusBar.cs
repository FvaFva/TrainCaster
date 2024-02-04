using System;
using System.Collections.Generic;

public class EnemyStatusBar
{
    private List<EnemyStatus> _enemyStatuses;

    public EnemyStatusParameters SummaryChanges;

    public EnemyStatusBar()
    {
        _enemyStatuses = new List<EnemyStatus>();
    }

    public event Action<float> HitPointsTick;
    public event Action StaticChanged;

    public void ApplyStatus(EnemyStatusParameters changing)
    {
        EnemyStatus enemyStatus = new EnemyStatus(changing);
        enemyStatus.HitPointsTick += HitPointsTick;
        _enemyStatuses.Add(enemyStatus);
        UpdateChanges(0);
    }

    public void UpdateChanges(float dt)
    {
        EnemyStatusParameters newStatus = new EnemyStatusParameters();

        for(int i = _enemyStatuses.Count - 1; i >= 0; i--)
        {
            _enemyStatuses[i].Tick(dt);

            if (_enemyStatuses[i].TimeLeft > 0)
            {
                newStatus += _enemyStatuses[i].Changing;
            }
            else
            {
                _enemyStatuses[i].HitPointsTick -= HitPointsTick;
                _enemyStatuses.RemoveAt(i);
            }
        }

        if (newStatus.IsSameStaticElements(SummaryChanges) == false)
        {
            SummaryChanges = newStatus;
            StaticChanged?.Invoke();
        }
    }
}