using UnityEngine;

[CreateAssetMenu(fileName = "RandomEnemySelector", menuName = "Spells/AdditionalSelectors/RandomEnemySelector", order = 51)]
public class RandomEnemySelector : BaseEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, SpellPartRarities rarity)
    {
        return new CastTarget(Enemies.GetRandom(target.Enemies), true);
    }
}