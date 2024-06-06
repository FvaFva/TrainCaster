using System;
public class EnemyStatus
{
    private const float HealthChangeInterval = 0.5f;

    private float _healthChangeTimer = 0;
    private float _fixDamagePerInterval;
    private Action<float> _hitMethod;

    public EnemyStatus(EnemyStatusParameters changing)
    {
        Duration = changing.Duration;
        Changing = changing;
        TimeLeft = changing.Duration;
        _fixDamagePerInterval = Changing.FixDamagePerSeconds * HealthChangeInterval;

        if (_fixDamagePerInterval != 0)
            _hitMethod = UpdateHealthChange;
        else
            _hitMethod = NonHealthChange;
    }

    public EnemyStatusParameters Changing {  get; private set; }

    public float Duration { get; private set; }
    public float TimeLeft { get; private set; }
    public event Action<float> HitPointsTick;

    public void Tick(float dt)
    {
        TimeLeft -= dt;
        _hitMethod.Invoke(dt);
    }

    private void UpdateHealthChange(float dt)
    {
        _healthChangeTimer -= dt;

        if (_healthChangeTimer <= 0)
        {
            _healthChangeTimer = HealthChangeInterval;
            HitPointsTick?.Invoke(_fixDamagePerInterval);
        }
    }

    private void NonHealthChange(float dt) { }
}