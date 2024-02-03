using System;

public class EnemyStatus
{
    private const float StartTimer = 0.5f;

    private float _timer = 0;
    private float _hitPointsTick;
    private Action<float> _hitPointsUpdate;

    public EnemyStatus(EnemyStatusParameters changing)
    {
        Lifetime = changing.Duration;
        Changing = changing;
        CurrentTime = changing.Duration;
        _hitPointsTick = Changing.HitPoints / StartTimer;

        if (_hitPointsTick != 0)
            _hitPointsUpdate = HealthUpdate;
        else
            _hitPointsUpdate = NonHealth;
    }

    public EnemyStatusParameters Changing {  get; private set; }

    public float Lifetime { get; private set; }
    public float CurrentTime { get; private set; }
    public event Action<float> HitPointsTick;

    public void Tick(float dt)
    {
        CurrentTime -= dt;
        _hitPointsUpdate.Invoke(dt);
    }

    private void HealthUpdate(float dt)
    {
        if(_timer <= 0)
        {
            _timer = StartTimer;
            HitPointsTick?.Invoke(_hitPointsTick * StartTimer);
        }
        else
        {
            _timer -= dt;
        }
    }

    private void NonHealth(float dt) { }
}