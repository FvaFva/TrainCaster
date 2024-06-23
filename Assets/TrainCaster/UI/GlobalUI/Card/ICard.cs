using UnityEngine;

public interface ICard
{
    public string Description { get; }
    public string Name { get; }
    public Sprite Icon { get; }

    public SpellPartRarities Rarity { get; }
}