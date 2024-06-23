using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Spells/Spell", order = 51)]
public class SpellBuild : ScriptableObject, ISpellBuild
{
    [SerializeField] private TargetSelector _targetSelector;
    [SerializeField] private BaseSpellEffect _effect;
    [SerializeField] private List<BaseSpellAction> _actions;
    [SerializeField] private List<EnemyStatusParameters> _statuses;
    [SerializeField] private BaseEnemySelector _enemySelector;
    [SerializeField] private SpellPartRarities _rarity;

    private Dictionary<SpellPart, SpellPartRarities> _rarities;

    public IEnumerable<BaseSpellAction> Actions => _actions;
    public IEnumerable<EnemyStatusParameters> Statuses => _statuses;
    public BaseSpellEffect Effect => _effect;
    public BaseEnemySelector EnemySelector => _enemySelector;
    public TypesSelection TargetSelector => _targetSelector.Selector;
    public Sprite Icon => _targetSelector.Icon;
    public string Name => _targetSelector.Header;
    public string Description => _targetSelector.Description;
    public SpellPartRarities Rarity => _rarity;

    public void Init()
    {
        _effect.InitResources();

        _rarities = new Dictionary<SpellPart, SpellPartRarities>
        {
            { _targetSelector, _rarity },
            { _effect, _rarity },
            { _enemySelector, _rarity }
        };

        foreach (SpellPart action in _actions)
        {
            if (_rarities.ContainsKey(action) == false)
                _rarities.Add(action, _rarity);
        }
    }

    public SpellPartRarities GetSpellPartRarity(SpellPart part)
    {
        if (_rarities.ContainsKey(part))
            return _rarities[part];
        else
            return SpellPartRarities.Common;
    }
}
