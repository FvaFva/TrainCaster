using UnityEngine;

[CreateAssetMenu(fileName = "NearestEnemySelector", menuName = "Spells/AdditionalSelectors/NearestEnemySelector", order = 51)]
public class NearestEnemySelector : BaseEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, SpellPartRarities rarity)
    {
        for (int i = 0; i < (int)rarity; i++)
        {
            target.ApplyEnemy(Enemies.GetNearest(target.Point, target.Enemies));
        }

        return target;
    }
}