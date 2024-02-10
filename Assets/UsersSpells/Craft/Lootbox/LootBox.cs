using System.Collections.Generic;
using UnityEngine;

public class LootBox : Interactable
{
    [Header("Loot box settings")]
    [SerializeField] private List<LootBoxSlot> _drop;

    private int _totalWeight = 0;

    public SpellPart Open()
    {
        int dicedValue = Random.Range(0, _totalWeight);

        foreach (LootBoxSlot slot in _drop)
        {
            if (slot.Range > dicedValue)
                return slot.SpellPart;
        }

        return _drop[0].SpellPart;
    }

    protected override void AwakeLoad()
    {
        for(int i = 0; i < _drop.Count; i++)
        {
            _totalWeight += _drop[i].Weight;
            _drop[i] = _drop[i].SetRange(_totalWeight);
        }

        if (_totalWeight == 0)
            gameObject.SetActive(false);
    }
}
