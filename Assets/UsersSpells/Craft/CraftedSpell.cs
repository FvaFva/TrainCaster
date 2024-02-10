using System.Collections.Generic;
using UnityEngine;

public struct CraftedSpell : ISpellBuild
{
    public TypesSelection TargetSelector {  get; private set; }

    public BaseSpellEffect Effect { get; private set; }

    public IEnumerable<BaseSpellAction> Actions { get; private set; }

    public IEnumerable<EnemyStatusParameters> Statuses { get; private set; }

    public Sprite Icon { get; private set; }

    public string Header { get; private set; }

    public BaseAdditionalEnemySelector EnemySelector { get; private set; }

    public int Count { get; private set; }

    public float Radius { get; private set; }
}