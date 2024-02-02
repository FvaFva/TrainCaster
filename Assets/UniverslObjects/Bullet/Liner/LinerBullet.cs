using UnityEngine;

public class LinerBullet : BaseBullet
{
    [SerializeField] private int _maxDistance;

    private Vector3 _direction;
    private float _currentDistance;

    protected override Vector3 CalculateNextStep(float deltaTime)
    {
        _currentDistance += deltaTime * Speed;
        return Speed * deltaTime * _direction;
    }

    protected override bool IsCanFly()
    {
        return _currentDistance < _maxDistance;
    }

    protected override bool IsItCorrectTarget(GameObject stoper)
    {
        return true;
    }

    protected override void ShotImpact(Vector3 position, CastTarget target)
    {
        _currentDistance = 0;
        Vector3 direction = target.Point - position;
        direction.y = 0;
        _direction = direction.normalized;
    }
}
