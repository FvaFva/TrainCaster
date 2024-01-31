using UnityEngine;

[CreateAssetMenu(fileName = "AllEnemySelector", menuName = "Spells/AdditionalSelectors/AllEnemySelector", order = 51)]
public class AllAdditionalEnemySelector : BaseAdditionalEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, int count = 0, float radius = 0)
    {
        foreach (EnemyRouter enemy in Enemies.AllEnemies)
            target.ApplyEnemy(enemy);

        return target;
    }
}