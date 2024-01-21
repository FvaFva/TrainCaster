using System;

public class HitPoints: IValueSource
{
    private float _regeneration;
    
    public HitPoints(float max, float regen) 
    {
        _regeneration = regen;
        Max = max;
        Current = Max;
    }

    public float Current {  get; private set; }
    public float Max { get; private set; }

    public event Action Die;
    public event Action Changed;

    public void SetNew(float value)
    {
        Current = value;
        Max = value;
        Changed?.Invoke();
    }

    public void ApplyDamage(float damage)
    {
        float realDamage = Math.Clamp(damage, 0, Current);
        Current -= realDamage;
        Changed?.Invoke();

        if (Current <= 0)
            Die?.Invoke();
    }

    public void Regenerate()
    {
        if (Current <= 0) 
            return;

        Current += _regeneration;
    }
}