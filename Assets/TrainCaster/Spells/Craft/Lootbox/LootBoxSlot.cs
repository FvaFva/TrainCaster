using System;
using UnityEngine;

[Serializable]
public struct LootBoxSlot
{
    [Range (0, 100)]public int Weight;
    public SpellPart SpellPart;
    public int Range {  get; private set; }

    public LootBoxSlot SetRange(int startPoint)
    {
        Range = startPoint;
        return this;
    }
}