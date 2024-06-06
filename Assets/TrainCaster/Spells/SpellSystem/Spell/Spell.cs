using UnityEngine;

public class Spell
{
    private ISpellBuild _build;

    public Spell(ISpellBuild build)
    {
        _build = build;
        _build.Effect.EffectFinished += EffectFinished;
    }

    public void Cast(Vector3 castPoint, CastTarget target)
    {
        CastTarget reconfiguredTarget = _build.EnemySelector.ProcessCastTarget(target, _build.GetSpellPartRarity(_build.EnemySelector));
        _build.Effect.Apply(castPoint, reconfiguredTarget);
    }

    private void EffectFinished(CastTarget castTarget)
    {
        foreach (BaseSpellAction action in _build.Actions)
            action.Apply(castTarget);
        

        foreach(EnemyStatusParameters statusParameters in _build.Statuses)
        {
            foreach(EnemyRouter enemy in castTarget.Enemies)
                enemy.ApplyStatus(statusParameters);
            
        }
    }
}