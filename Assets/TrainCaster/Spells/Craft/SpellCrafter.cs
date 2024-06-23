using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpellCrafter : MonoBehaviour, IInventorySource
{
    [SerializeField] private Train _train;
    [SerializeField] private SpellSlot _slot;
    [SerializeField] private Button _craftButton;

    [Inject] private Inventory<SpellElement> _inventory;

    private Dictionary<SpellPartTypes, SpellElement> _spellParts;
    private Wagon _wagon;

    public event Action<IEnumerable<SpellElement>> Changed;
    public event Action<ICard> Mined;

    private void Awake()
    {
        _spellParts = new Dictionary<SpellPartTypes, SpellElement>();

        foreach (SpellPartTypes type in Enum.GetValues(typeof(SpellPartTypes)))
            _spellParts.Add(type, null);
    }

    private void OnEnable()
    {
        _inventory.Chose += AddPart;
        _train.ChoseNewWagons += OnWagonChanged;
        _craftButton.onClick.AddListener(Craft);
    }

    private void OnDisable()
    {
        _inventory.Chose -= AddPart;
        _train.ChoseNewWagons -= OnWagonChanged;
        _craftButton.onClick.RemoveListener(Craft);
    }

    private void Craft()
    {
        if (_wagon == null)
            return;

        CraftedSpell newSpell = new CraftedSpell();

        foreach(SpellPartTypes type in Enum.GetValues(typeof(SpellPartTypes)))
        {
            if (_spellParts[type] == null)
                continue;

            _spellParts[type].Accept(newSpell);
            _inventory.Remove(_spellParts[type]);
            _spellParts[type] = null;
        }

        _wagon.ApplySpellBuild(newSpell);
        Changed?.Invoke(_spellParts.Values);
        Mined?.Invoke(newSpell);
    }

    private void AddPart(SpellElement part)
    {
        if (_spellParts[part.Type] == part)
            _spellParts[part.Type] = null;
        else
            _spellParts[part.Type] = part;

        Changed?.Invoke(_spellParts.Values);
    }

    private void OnWagonChanged(Wagon wagon1, Wagon wagon2)
    {
        _wagon = wagon1;
        _slot.ApplyWagon(_wagon);
    }
}