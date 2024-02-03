using System;

[Serializable]
public struct EnemyStatusParameters
{
    public float Defense;
    public float HitPoints;
    public float MoveSpeed;
    public float Duration;

    public bool IsSameStaticElements(EnemyStatusParameters another)
    {
        return Defense.Equals(another.Defense) && HitPoints.Equals(another.HitPoints);
    }

    public static EnemyStatusParameters operator + (EnemyStatusParameters left, EnemyStatusParameters right)
    {
        EnemyStatusParameters result = new EnemyStatusParameters();
        result.Defense = left.Defense + right.Defense;
        result.HitPoints = left.HitPoints + right.HitPoints;
        result.MoveSpeed = left.MoveSpeed + right.MoveSpeed;
        return result;
    }
}
