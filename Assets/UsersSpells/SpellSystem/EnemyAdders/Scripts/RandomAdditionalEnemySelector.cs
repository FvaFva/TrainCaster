using UnityEngine;

[CreateAssetMenu(fileName = "RandomEnemySelector", menuName = "Spells/AdditionalSelectors/RandomEnemySelector", order = 51)]
public class RandomAdditionalEnemySelector : BaseAdditionalEnemySelector
{
    public override CastTarget ProcessCastTarget(CastTarget target, int count, float radius)
    {
        return new CastTarget(Enemies.GetRandom(target.Enemies), true);
    }
}