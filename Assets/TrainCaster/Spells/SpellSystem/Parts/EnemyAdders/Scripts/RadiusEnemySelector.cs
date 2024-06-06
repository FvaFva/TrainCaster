using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadiusEnemySelector", menuName = "Spells/AdditionalSelectors/RadiusEnemySelector", order = 51)]
public class RadiusEnemySelector : BaseEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, SpellPartRarities rarity)
    {
        IEnumerator<EnemyRouter> inRadius = Enemies.GetAllInRadius(target.Point, (int)rarity, target.Enemies)?.GetEnumerator();

        if (inRadius == null)
            return target;

        while(inRadius.MoveNext())
            target.ApplyEnemy(inRadius.Current);

        return target;
    }
}