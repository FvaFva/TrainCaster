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
        SpellPart result = _drop[0].SpellPart;

        foreach (LootBoxSlot slot in _drop)
        {
            if (slot.Range > dicedValue)
                result = slot.SpellPart;
        }

        Deactivate();
        return result;
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
