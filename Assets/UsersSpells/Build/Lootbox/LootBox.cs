using System.Collections.Generic;
using UnityEngine;

public class LootBox : Interactable
{
    [Header("Loot box settings")]
    [SerializeField] private List<LootBoxSlot> _drop;

    private int _totalWeight = 0;

    private void Awake()
    {
        foreach (LootBoxSlot slot in _drop)
        {
            _totalWeight += slot.Weight;
            slot.SetRange(_totalWeight);
        }

        if(_totalWeight == 0)
            gameObject.SetActive(false);
    }

    public ISpellPart Open()
    {
        int dicedValue = Random.Range(0, _totalWeight);

        foreach (LootBoxSlot slot in _drop)
        {
            if (slot.Range > dicedValue)
                return slot.SpellPart;
        }

        return _drop[0].SpellPart;
    }
}
