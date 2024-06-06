using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemySelector", menuName = "Spells/AdditionalSelectors/AllEnemySelector", order = 51)]
public class AllEnemySelector : BaseEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, SpellPartRarities rarity)
    {
        foreach (EnemyRouter enemy in Enemies.AllEnemies)
            target.ApplyEnemy(enemy);

        return target;
    }
}