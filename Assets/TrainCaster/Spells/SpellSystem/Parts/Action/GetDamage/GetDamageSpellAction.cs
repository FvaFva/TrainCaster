using UnityEngine;

[CreateAssetMenu(fileName = "Hit", menuName = "Spells/Actions/Hit", order = 51)]
class GetDamageSpellAction : BaseSpellAction
{
    [SerializeField] private int _damage;

    public override void Apply(CastTarget target)
    {
        foreach(EnemyRouter enemy in target.Enemies)
            enemy.ApplyDamage(_damage);
    }
}