using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellBar : MonoBehaviour
{
    [SerializeField] private SpellSlot _firstSlot;
    [SerializeField] private SpellSlot _secondSlot;
    [SerializeField] private Train _train;

    private Dictionary<Guid, SpellSlot> _slots;

    private void Awake()
    {
        _slots = new Dictionary<Guid, SpellSlot>();
    }

    private void OnEnable()
    {
        _train.ChoseNewWagons += UpdateWagonsInSlots;
    }

    private void OnDisable()
    {
        _train.ChoseNewWagons -= UpdateWagonsInSlots;
    }

    public void BindSlotToAction(Guid guid)
    {
        if (_slots.Count == 2)
            return;
        else if (_slots.ContainsValue(_firstSlot))
            _slots.Add(guid, _secondSlot);
        else
            _slots.Add(guid, _firstSlot);
    }

    public bool IsActive(Guid guid)
    {
        return _slots.GetValueOrDefault(guid).IsActive;
    }

    public TypesSelection GetSelector(Guid guid)
    {
        return _slots.GetValueOrDefault(guid).SelectionType;
    }

    public void Cast(Guid guid, CastTarget target)
    {
        _slots.GetValueOrDefault(guid).Cast(target);
    }

    public Transform GetCastPoint(Guid guid)
    {
        return _slots.GetValueOrDefault(guid).CastPoint;
    }

    private void UpdateWagonsInSlots(Wagon wagonA, Wagon wagonB)
    {
        _firstSlot.ApplyWagon(wagonA);
        _secondSlot.ApplyWagon(wagonB);
    }
}
