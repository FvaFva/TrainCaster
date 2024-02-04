using System.Collections.Generic;
using UnityEngine;

public interface ISpellBuild
{
    public TypesSelection TargetSelector {  get; }
    public BaseSpellEffect Effect { get; }
    public IEnumerable<BaseSpellAction> Actions { get; }
    public IEnumerable<EnemyStatusParameters> Statuses { get; }
    public Sprite Icon { get; }
    public string Header { get; }
    public BaseAdditionalEnemySelector EnemySelector { get; }
    public int Count { get; }
    public float Radius { get; }
}
