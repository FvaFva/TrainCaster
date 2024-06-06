using UnityEngine;

public class SpellElement: ISpellElement
{
    private SpellPart _part;

    public SpellElement(SpellPart part, SpellPartRarities rarity)
    {
        _part = part;
        Rarity = rarity;
    }

    public string Description => _part.Description;

    public string Name => _part.Header;

    public Sprite Icon => _part.Icon;

    public SpellPartTypes Type => _part.Type;

    public SpellPartRarities Rarity { get; private set; }

    public void Accept(ISpellPartVisitor visitor)
    {
        _part.Accept(visitor, Rarity);
    }
}