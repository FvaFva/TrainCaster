using UnityEngine;

[CreateAssetMenu(fileName = "new Spell root", menuName = "Spells/Root/SpellRoot", order = 51)]
public class SpellRoot : SpellPart
{
    [Header("Spell root settings")]
    [SerializeField] private TypesSelection _selector;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _header;
    [SerializeField] private int _count;
    [SerializeField] private float _radius;

    public override SpellPartTypes Type => SpellPartTypes.Root;

    public TypesSelection TargetSelector => _selector;

    public Sprite Icon => _icon;

    public string Header => _header;

    public int Count => _count;

    public float Radius => _radius;
}
