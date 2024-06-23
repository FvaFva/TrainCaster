public interface ISpellElement: ICard
{
    public SpellPartTypes Type { get; }
    public SpellPartRarities Rarity { get; }
}