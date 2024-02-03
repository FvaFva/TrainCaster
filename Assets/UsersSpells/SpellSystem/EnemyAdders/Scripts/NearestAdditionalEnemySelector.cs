using UnityEngine;

[CreateAssetMenu(fileName = "NearestEnemySelector", menuName = "Spells/AdditionalSelectors/NearestEnemySelector", order = 51)]
public class NearestAdditionalEnemySelector : BaseAdditionalEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, int count, float radius)
    {
        for (int i = 0; i < count; i++)
        {
            target.ApplyEnemy(Enemies.GetNearest(target.Point, target.Enemies));
        }

        return target;
    }
}