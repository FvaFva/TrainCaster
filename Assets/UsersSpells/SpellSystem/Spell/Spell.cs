using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spell
{
    private SpellBuild _build;

    public Spell(SpellBuild build)
    {
        _build = build;
    }

    public void Cast(Vector3 castPoint, CastTarget target)
    {
        _build.Effect.Apply(castPoint, target, EffectFinished);
    }

    private void EffectFinished(CastTarget castTarget)
    {
        foreach (BaseSpellAction action in _build.Actions)
        {
            action.Apply(castTarget);
        }
    }
}