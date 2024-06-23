using System.Collections.Generic;
using UnityEngine;

public class CraftedSpell : ISpellBuild, ISpellPartVisitor
{
    private List<BaseSpellAction> _actions = new List<BaseSpellAction>();
    private List<EnemyStatusParameters> _statuses = new List<EnemyStatusParameters>();
    private TargetSelector _root;
    private TypesSelection _targetSelector;
    private Dictionary<SpellPart, SpellPartRarities> _partsRarities = new Dictionary<SpellPart, SpellPartRarities>();

    public IEnumerable<BaseSpellAction> Actions => _actions;

    public IEnumerable<EnemyStatusParameters> Statuses => _statuses;

    public BaseSpellEffect Effect { get; private set; }

    public BaseEnemySelector EnemySelector { get; private set; }

    public TypesSelection TargetSelector => _targetSelector;

    public Sprite Icon => _root.Icon;

    public string Name => _root.Header;

    public string Description => _root.Description;

    public SpellPartRarities Rarity { get; private set; }

    public CraftedSpell()
    {
        Rarity = SpellPartRarities.Common;
    }

    public void Visit(TargetSelector part, SpellPartRarities rarity)
    {
        _targetSelector = part ? TypesSelection.Non : part.Selector;
        _root = part;
        OnVisit(part, rarity);
    }

    public void Visit(BaseEnemySelector part, SpellPartRarities rarity)
    {
        EnemySelector = part;
        OnVisit(part, rarity);
    }

    public void Visit(BaseSpellEffect part, SpellPartRarities rarity)
    {
        Effect = part;
        OnVisit(part, rarity);
    }

    public void Visit(BaseSpellAction part, SpellPartRarities rarity)
    {
        _actions.Add(part);
        OnVisit(part, rarity);
    }

    public void AddEnemyStatus(EnemyStatusParameters statusParameters)
    {
        _statuses.Add(statusParameters);
    }

    public void Init()
    {
        Effect.InitResources();
    }

    public SpellPartRarities GetSpellPartRarity(SpellPart part)
    {
        if (_partsRarities.ContainsKey(part))
            return _partsRarities[part];
        else
            return SpellPartRarities.Common;
    }

    private void OnVisit(SpellPart part, SpellPartRarities rarity)
    {
        _partsRarities.Add(part, rarity);

        if ((int)Rarity > (int)rarity)
            Rarity = rarity;
    }
}