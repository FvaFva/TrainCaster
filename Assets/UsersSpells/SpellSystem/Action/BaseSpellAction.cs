public abstract class BaseSpellAction : SpellPart
{
    public abstract void Apply(CastTarget target);
    public override SpellPartTypes Type => SpellPartTypes.Action;
}