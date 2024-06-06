using Zenject;

public abstract class BaseEnemySelector : SpellPart
{
    [Inject] protected ActiveEnemies Enemies { get; private set; }

    public abstract CastTarget ProcessCastTarget(CastTarget target, SpellPartRarities rarity);

    public override SpellPartTypes Type => SpellPartTypes.EnemyAdder;

    public override void Accept(ISpellPartVisitor visitor, SpellPartRarities rarity)
    {
        visitor.Visit(this, rarity);
    }
}