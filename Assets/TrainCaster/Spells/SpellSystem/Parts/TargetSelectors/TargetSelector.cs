using UnityEngine;

[CreateAssetMenu(fileName = "New target selector", menuName = "Spells/Target selector", order = 51)]
public class TargetSelector : SpellPart
{
    [Header("Target selector settings")]
    [SerializeField] private TypesSelection _selector;

    public override SpellPartTypes Type => SpellPartTypes.Root;

    public TypesSelection Selector => _selector;

    public override void Accept(ISpellPartVisitor visitor, SpellPartRarities rarity)
    {
        visitor.Visit(this, rarity);
    }
}
