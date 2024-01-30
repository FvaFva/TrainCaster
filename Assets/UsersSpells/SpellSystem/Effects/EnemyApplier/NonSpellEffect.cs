using System;
using UnityEngine;

public class NonSpellEffect : BaseSpellEffect
{ 
    public override void Apply(Vector3 castPoint, CastTarget target, Action<CastTarget> onEffectFinish)
    {
        onEffectFinish?.Invoke(target);
    }

    public override void InitResources()
    {
    }
}