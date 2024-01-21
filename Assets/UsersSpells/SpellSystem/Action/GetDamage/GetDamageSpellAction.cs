using UnityEngine;

[CreateAssetMenu(fileName = "Hit", menuName = "Spells/Actions/Hit", order = 51)]
class GetDamageSpellAction : BaseSpellAction
{
    [SerializeField] private int _damage;

    public override void Apply(CastTarget target)
    {
        if (target.Enemy != null)
            target.Enemy.ApplyDamage(_damage);
    }
}