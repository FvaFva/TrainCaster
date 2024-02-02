using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RadiusEnemySelector", menuName = "Spells/AdditionalSelectors/RadiusEnemySelector", order = 51)]
public class RadiusAdditionalEnemySelector : BaseAdditionalEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, int count, float radius)
    {
        IEnumerator<EnemyRouter> inRadius = Enemies.GetAllInRadius(target.Point, radius, target.Enemies)?.GetEnumerator();

        for(int i = count; inRadius != null && i > 0 && inRadius.MoveNext(); i--) 
            target.ApplyEnemy(inRadius.Current);
            
        return target;
    }
}