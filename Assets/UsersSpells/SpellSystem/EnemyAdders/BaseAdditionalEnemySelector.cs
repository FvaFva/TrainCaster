using Zenject;

public abstract class BaseAdditionalEnemySelector : SpellPart
{
    [Inject] protected ActiveEnemies Enemies { get; private set; }
    public abstract CastTarget ProcessCastTarget(CastTarget target, int count, float radius);

    public override SpellPartTypes Type => SpellPartTypes.EnemyAdder;
}