public abstract class BaseSpellAction : SpellPart
{
    public abstract void Apply(CastTarget target);
    public override SpellPartTypes Type => SpellPartTypes.Action;

    public override void Accept(ISpellPartVisitor visitor, SpellPartRarities rarity)
    {
        visitor.Visit(this, rarity);
    }
}