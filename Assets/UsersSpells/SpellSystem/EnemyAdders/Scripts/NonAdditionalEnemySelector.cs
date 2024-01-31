using UnityEngine;

[CreateAssetMenu(fileName = "NonEnemySelector", menuName = "Spells/AdditionalSelectors/NonEnemySelector", order = 51)]
public class NonAdditionalEnemySelector : BaseAdditionalEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, int count = 0, float radius = 0)
    {
        return target;
    }
}