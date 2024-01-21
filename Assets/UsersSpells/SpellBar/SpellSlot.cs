using System;
using UnityEngine;

public class SpellSlot : MonoBehaviour
{
    private Wagon _wagon;

    public TypesSelection SelectionType {  get; private set; }
    public Transform CastPoint {  get; private set; }
    public bool IsActive { get; private set; }

    public event Action<SpellBuild> UpdatedSpell;

    public void Cast(CastTarget target)
    {
        _wagon.Cast(target);
    }

    public void ApplyWagon(Wagon wagon)
    {
        _wagon = wagon;

        if (_wagon == null)
        {
            IsActive = false;
            UpdatedSpell?.Invoke(default);
            return;
        }
        else
        {
            IsActive = true;
            UpdatedSpell?.Invoke(_wagon.Spell);
            SelectionType = wagon.Spell.TargetSelector;
            CastPoint = wagon.CastPoint;
        }
    }
}
