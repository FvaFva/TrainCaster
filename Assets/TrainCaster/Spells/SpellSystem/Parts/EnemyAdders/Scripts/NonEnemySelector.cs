using UnityEngine;

[CreateAssetMenu(fileName = "NonEnemySelector", menuName = "Spells/AdditionalSelectors/NonEnemySelector", order = 51)]
public class NonEnemySelector : BaseEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, SpellPartRarities rarity)
    {
        return target;
    }
}