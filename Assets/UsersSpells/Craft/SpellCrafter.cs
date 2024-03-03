using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpellCrafter : MonoBehaviour
{
    [Inject] private SpellInventory _inventory;

    private Dictionary<SpellPartTypes, SpellPart> _spellParts;

    public event Action<IEnumerable<SpellPart>> Changed;

    private void Awake()
    {
        _spellParts = new Dictionary<SpellPartTypes, SpellPart>();

        foreach (SpellPartTypes type in Enum.GetValues(typeof(SpellPartTypes)))
            _spellParts.Add(type, null);
    }

    private void OnEnable()
    {
        _inventory.Chose += AddPart;
    }

    private void OnDisable()
    {
        _inventory.Chose -= AddPart;
    }

    public ISpellBuild Craft()
    {
        CraftedSpell newSpell = new CraftedSpell(_spellParts[SpellPartTypes.Effect] as BaseSpellEffect, _spellParts[SpellPartTypes.EnemyAdder] as BaseAdditionalEnemySelector, _spellParts[SpellPartTypes.Root] as SpellRoot);
        newSpell.AddSpellAction(_spellParts[SpellPartTypes.Action] as BaseSpellAction);

        foreach(SpellPartTypes type in _spellParts.Keys)
        {
            if (_spellParts[type] == null)
                continue;

            _inventory.Remove(_spellParts[type]);
            _spellParts[type] = null;
        }

        Changed?.Invoke(_spellParts.Values);
        return newSpell;
    }

    private void AddPart(SpellPart part)
    {
        _spellParts[part.Type] = part;
        Changed?.Invoke(_spellParts.Values);
    }
}