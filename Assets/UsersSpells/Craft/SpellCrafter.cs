using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellCrafter : MonoBehaviour
{
    [SerializeField] LootBoxUnpacker _unpacker;

    private SpellInventory _inventory;
    private Dictionary<SpellPartTypes, SpellPart> _spellParts;

    private void Awake()
    {
        _inventory = new SpellInventory();
        _spellParts = new Dictionary<SpellPartTypes, SpellPart>();

        foreach (SpellPartTypes type in Enum.GetValues(typeof(SpellPartTypes)))
            _spellParts.Add(type, null);
    }

    private void OnEnable()
    {
        _unpacker.OnOpen += OnOpenBox;
    }

    private void OnDisable()
    {
        _unpacker.OnOpen -= OnOpenBox;
    }

    public ISpellBuild Craft()
    {
        CraftedSpell newSpell = new CraftedSpell(_spellParts[SpellPartTypes.Effect] as BaseSpellEffect, _spellParts[SpellPartTypes.EnemyAdder] as BaseAdditionalEnemySelector, _spellParts[SpellPartTypes.Root] as SpellRoot);
        newSpell.AddSpellAction(_spellParts[SpellPartTypes.Action] as BaseSpellAction);
        return newSpell;
    }

    private void OnOpenBox(SpellPart spellPart)
    {
        Debug.Log($"Box opened!!! Drop: {spellPart.name} - {spellPart.Description}");
        _spellParts[spellPart.Type] = spellPart;
    }
}