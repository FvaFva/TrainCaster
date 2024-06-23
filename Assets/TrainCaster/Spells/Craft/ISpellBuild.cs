using System.Collections.Generic;

public interface ISpellBuild: IInitialized, ICard
{
    public TypesSelection TargetSelector {  get; }
    public BaseSpellEffect Effect { get; }
    public IEnumerable<BaseSpellAction> Actions { get; }
    public IEnumerable<EnemyStatusParameters> Statuses { get; }
    public BaseEnemySelector EnemySelector { get; }
    public SpellPartRarities GetSpellPartRarity(SpellPart part);
}
