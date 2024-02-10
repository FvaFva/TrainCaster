using System;
using UnityEngine;

public abstract class BaseSpellEffect: SpellPart
{
    public abstract void Apply(Vector3 castPoint, CastTarget target, Action<CastTarget> onEffectFinish);

    public virtual void InitResources() { }

    public override SpellPartTypes Type => SpellPartTypes.Effect;
}