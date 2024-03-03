using UnityEngine;

public abstract class SpellPart : ScriptableObject, ISpellPart
{
    [Header("Spell part settings")]
    [SerializeField] private string _description;

    public string Description => _description;
    public abstract SpellPartTypes Type { get; }
}