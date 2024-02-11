using System.Collections.Generic;
using UnityEngine;

public struct CraftedSpell : ISpellBuild
{
    private List<BaseSpellAction> _actions;
    private List<EnemyStatusParameters> _statuses;
    private SpellRoot _root;
    private TypesSelection _targetSelector;

    public CraftedSpell(BaseSpellEffect effect, BaseAdditionalEnemySelector enemySelector, SpellRoot root)
    {
        _actions = new List<BaseSpellAction>();
        _statuses = new List<EnemyStatusParameters>();
        Effect = effect;
        EnemySelector = enemySelector;
        _targetSelector = root? TypesSelection.Non : root.TargetSelector;
        _root = root;
    }

    public IEnumerable<BaseSpellAction> Actions => _actions;

    public IEnumerable<EnemyStatusParameters> Statuses => _statuses;

    public BaseSpellEffect Effect { get; private set; }

    public BaseAdditionalEnemySelector EnemySelector { get; private set; }


    public TypesSelection TargetSelector => _targetSelector;

    public Sprite Icon => _root.Icon;

    public string Header => _root.Header;

    public int Count => _root.Count;

    public float Radius => _root.Radius;

    public void AddSpellAction (BaseSpellAction action)
    {
        _actions.Add(action);
    }

    public void AddEnemyStatus(EnemyStatusParameters statusParameters)
    {
        _statuses.Add(statusParameters);
    }
}