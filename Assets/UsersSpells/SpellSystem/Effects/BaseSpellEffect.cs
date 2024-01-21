using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpellEffect: ScriptableObject
{
    public abstract void Apply(Vector3 castPoint, CastTarget target, Action<IEnumerable<CastTarget>> onEffectFinish);

    public abstract void InitResources();
}