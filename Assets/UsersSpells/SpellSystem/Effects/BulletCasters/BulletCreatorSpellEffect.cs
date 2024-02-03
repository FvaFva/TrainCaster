using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BulletCreator", menuName = "Spells/Effects/BulletCreator", order = 51)]
public class BulletCreatorSpellEffect : BaseSpellEffect
{
    [SerializeField] private BaseBullet _bullet;

    [Inject] private PoolService _poolService;

    public override void Apply(Vector3 castPoint, CastTarget target, Action<CastTarget> onEffectFinish)
    {
        foreach (EnemyRouter enemy in target.Enemies)
            InitBullet(castPoint, new CastTarget(enemy, target.IsCorrect), onEffectFinish);
    }

    public override void InitResources()
    {
        _poolService.Put<BaseBullet>(_bullet.gameObject, 30);
    }

    private void InitBullet(Vector3 castPoint, CastTarget target, Action<CastTarget> onEffectFinish)
    {
        BaseBullet bullet = _poolService.Get<BaseBullet>(_bullet.gameObject);
        bullet.Shot(castPoint, target, onEffectFinish);
    }
}