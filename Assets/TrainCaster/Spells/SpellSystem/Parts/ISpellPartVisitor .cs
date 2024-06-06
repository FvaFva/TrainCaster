public interface ISpellPartVisitor
{
    public void Visit(TargetSelector part, SpellPartRarities rarity);
    public void Visit(BaseSpellAction part, SpellPartRarities rarity);
    public void Visit(BaseSpellEffect part, SpellPartRarities rarity);
    public void Visit(BaseEnemySelector part, SpellPartRarities rarity);
}