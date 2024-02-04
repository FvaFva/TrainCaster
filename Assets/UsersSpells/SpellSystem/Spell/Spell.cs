using UnityEngine;

public class Spell
{
    private SpellBuild _build;

    public Spell(SpellBuild build)
    {
        _build = build;
    }

    public void Cast(Vector3 castPoint, CastTarget target)
    {
        _build.Effect.Apply(castPoint, _build.EnemySelector.ProcessCastTarget(target, _build.Count, _build.Radius), EffectFinished);
    }

    private void EffectFinished(CastTarget castTarget)
    {
        foreach (BaseSpellAction action in _build.Actions)
        {
            action.Apply(castTarget);
        }

        foreach(EnemyStatusParameters statusParameters in _build.Statuses)
        {
            foreach(EnemyRouter enemy in castTarget.Enemies)
            {
                enemy.ApplyStatus(statusParameters);
            }
        }
    }
}