using UnityEngine;

public abstract class SpellPart : ScriptableObject
{
    [Header("Spell part settings")]
    [SerializeField] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _header;

    public string Description => _description;
    public string Header => _header;
    public Sprite Icon => _icon;

    public abstract SpellPartTypes Type { get; }

    public abstract void Accept(ISpellPartVisitor visitor, SpellPartRarities rarity);
}