using System;

[Serializable]
public struct EnemyStatusParameters
{
    public float Armor;
    public float FixDamagePerSeconds;
    public float MoveSpeed;
    public float Duration;

    public bool IsSameStaticElements(EnemyStatusParameters another)
    {
        return Armor.Equals(another.Armor) && MoveSpeed.Equals(another.MoveSpeed);
    }

    public static EnemyStatusParameters operator + (EnemyStatusParameters left, EnemyStatusParameters right)
    {
        EnemyStatusParameters result = new EnemyStatusParameters();
        result.Armor = left.Armor + right.Armor;
        result.MoveSpeed = MathF.Max(left.MoveSpeed, right.MoveSpeed);
        result.FixDamagePerSeconds = left.FixDamagePerSeconds + right.FixDamagePerSeconds;
        return result;
    }
}
