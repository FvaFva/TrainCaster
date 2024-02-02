using UnityEngine;

public class TargetBullet : BaseBullet
{
    private EnemyRouter _target;

    protected override Vector3 CalculateNextStep(float deltaTime)
    {
        Vector3 direction = _target.Position - Position;
        direction.y = 0;
        return Speed * deltaTime * direction.normalized;
    }

    protected override bool IsCanFly()
    {
        return _target != null && _target.isActiveAndEnabled;
    }

    protected override bool IsItCorrectTarget(GameObject stoper)
    {
        return stoper.TryGetComponent<EnemyRouter>(out EnemyRouter temp) && _target == temp;
    }

    protected override void ShotImpact(Vector3 position, CastTarget target)
    {
        _target = target.Enemy;
        gameObject.SetActive(true);
    }
}
