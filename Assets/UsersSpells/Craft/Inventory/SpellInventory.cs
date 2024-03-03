using System;
using System.Collections.Generic;

public class SpellInventory
{
    private LootBoxUnpacker _unpacker;
    private List<SpellPart> _parts = new List<SpellPart>();

    public SpellInventory(LootBoxUnpacker unpacker)
    {
        _unpacker = unpacker;
        _unpacker.OnOpen += Add;
    }

    public IEnumerable<ISpellPart> Parts => _parts;

    public event Action Changed;
    public event Action<SpellPart> Chose;

    public void Remove(ISpellPart part)
    {
        SpellPart temp = part as SpellPart;

        if (_parts.Contains(temp))
        {
            _parts.Remove(temp);
            Changed?.Invoke();
        }
    }

    public void Choose(ISpellPart part)
    {
        SpellPart temp = part as SpellPart;

        if (_parts.Contains(temp))
            Chose?.Invoke(temp);
    }

    private void Add(SpellPart part)
    {
        _parts.Add(part);
        Changed?.Invoke();
    }
}