using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BulletCreator", menuName = "Spells/Effects/BulletCreator", order = 51)]
public class BulletCreatorSpellEffect : BaseSpellEffect
{
    [SerializeField] private BaseBullet _bullet;
    [Inject] private PoolService _poolService;

    public override void Apply(Vector3 castPoint, CastTarget target)
    {
        foreach (EnemyRouter enemy in target.Enemies)
            InitBullet(castPoint, new CastTarget(enemy, target.IsCorrect));
    }

    public override void InitResources()
    {
        _poolService.Put(_bullet, 30, Header);
    }

    private void InitBullet(Vector3 castPoint, CastTarget target)
    {
        BaseBullet bullet = _poolService.Resolve<BaseBullet>(Header);
        bullet.Shot(castPoint, target);
        bullet.Crushed += OnBulletCrushed;
    }

    private void OnBulletCrushed(CastTarget target, BaseBullet bullet)
    {
        bullet.Crushed -= OnBulletCrushed;
        Finished(target);
    }
}