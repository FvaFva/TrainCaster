using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spells/Spell", order = 51)]
public class SpellBuild : ScriptableObject, IInitialized
{
    [Header("Ui")]
    [SerializeField] private string _header;
    [SerializeField] private Sprite _icon;
    [Header("Select")]
    [SerializeField] private TypesSelection _targetSelector;
    [SerializeField] private BaseAdditionalEnemySelector _enemySelector;
    [SerializeField] private float _radius;
    [SerializeField] private int _count;
    [Header("Impact")]
    [SerializeField] private BaseSpellEffect _effect;
    [SerializeField] private List<BaseSpellAction> _actions;

    public TypesSelection TargetSelector => _targetSelector;
    public BaseSpellEffect Effect => _effect;
    public IEnumerable<BaseSpellAction> Actions => _actions;
    public Sprite Icon => _icon;
    public string Header => _header;
    public BaseAdditionalEnemySelector EnemySelector => _enemySelector;
    public int Count => _count;

    public void Init()
    {
        _effect.InitResources();
    }
}
