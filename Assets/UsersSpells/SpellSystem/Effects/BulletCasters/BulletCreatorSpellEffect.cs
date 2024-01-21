using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletCreator", menuName = "Spells/Effects/BulletCreator", order = 51)]
public class BulletCreatorSpellEffect : BaseSpellEffect
{
    [SerializeField] private BaseBullet _bullet;
    [SerializeField] private bool _freezeY;

    public override void Apply(Vector3 castPoint, CastTarget target, Action<IEnumerable<CastTarget>> onEffectFinish)
    {
        if (Storage.Instance.TryGetFree<BaseBullet>(_bullet.gameObject, out BaseBullet bullet))
        {
            Vector3 direction = target.Point - castPoint;
            direction.y = _freezeY ? 0 : direction.y;
            direction = Vector3.Normalize(direction);
            bullet.Shot(castPoint, direction, onEffectFinish);
        }
    }

    public override void InitResources()
    {
        Storage.Instance.Put(_bullet.gameObject, 30);
    }
}