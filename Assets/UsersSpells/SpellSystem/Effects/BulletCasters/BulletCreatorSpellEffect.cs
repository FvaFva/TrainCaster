using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BulletCreator", menuName = "Spells/Effects/BulletCreator", order = 51)]
public class BulletCreatorSpellEffect : BaseSpellEffect
{
    [SerializeField] private BaseBullet _bullet;
    [SerializeField] private bool _freezeY;

    [Inject] private PoolService _poolService;

    public override void Apply(Vector3 castPoint, CastTarget target, Action<CastTarget> onEffectFinish)
    {
        BaseBullet bullet = _poolService.Get<BaseBullet>(_bullet.gameObject);
        Vector3 direction = target.Point - castPoint;
        direction.y = _freezeY ? 0 : direction.y;
        direction = Vector3.Normalize(direction);
        bullet.Shot(castPoint, direction, onEffectFinish);
    }

    public override void InitResources()
    {
        _poolService.Put<BaseBullet>(_bullet.gameObject, 30);
    }
}