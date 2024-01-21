using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Abra cadabra", menuName = "Spells/Spell", order = 51)]
public class SpellBuild : ScriptableObject, IInitialized
{
    [Header("Ui")]
    [SerializeField] private string _header;
    [SerializeField] private Sprite _icon;
    [Header("Impact")]
    [SerializeField] private TypesSelection _targetSelector;
    [SerializeField] private BaseSpellEffect _effect;
    [SerializeField] private List<BaseSpellAction> _actions;

    public TypesSelection TargetSelector => _targetSelector;
    public BaseSpellEffect Effect => _effect;
    public IEnumerable<BaseSpellAction> Actions => _actions;
    public Sprite Icon => _icon;
    public string Header => _header;

    public void Init()
    {
        _effect.InitResources();
    }
}
