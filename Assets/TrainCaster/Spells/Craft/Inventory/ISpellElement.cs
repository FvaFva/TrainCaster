public interface ISpellElement: ICardSource
{
    public SpellPartTypes Type { get; }
    public SpellPartRarities Rarity { get; }
}