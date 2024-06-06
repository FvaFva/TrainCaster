using System;
using System.Collections.Generic;

public class SpellInventory
{
    private LootBoxUnpacker _unpacker;
    private List<SpellElement> _parts = new List<SpellElement>();

    public SpellInventory(LootBoxUnpacker unpacker)
    {
        _unpacker = unpacker;
        _unpacker.OnOpen += Add;
    }

    public IEnumerable<ISpellElement> Parts => _parts;

    public event Action Changed;
    public event Action<SpellElement> Chose;

    public void Remove(ISpellElement part)
    {
        SpellElement temp = part as SpellElement;

        if (_parts.Contains(temp))
        {
            _parts.Remove(temp);
            Changed?.Invoke();
        }
    }

    public void Choose(ISpellElement part)
    {
        SpellElement temp = part as SpellElement;

        if (_parts.Contains(temp))
            Chose?.Invoke(temp);
    }

    private void Add(SpellPart part, SpellPartRarities rarity)
    {
        _parts.Add(new SpellElement(part, rarity));
        Changed?.Invoke();
    }
}