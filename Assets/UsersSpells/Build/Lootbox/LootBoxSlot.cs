using System;

[Serializable]
public struct LootBoxSlot
{
    public int Weight;
    public ISpellPart SpellPart;
    public int Range {  get; private set; }

    public void SetRange(int startPoint)
    {
        Range = startPoint;
    }
}