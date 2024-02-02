using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NonEffect", menuName = "Spells/Effects/NonEffect", order = 51)]
public class NonSpellEffect : BaseSpellEffect
{ 
    public override void Apply(Vector3 castPoint, CastTarget target, Action<CastTarget> onEffectFinish)
    {
        onEffectFinish?.Invoke(target);
    }
}