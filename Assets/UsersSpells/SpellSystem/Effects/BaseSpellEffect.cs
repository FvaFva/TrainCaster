using System;
using UnityEngine;

public abstract class BaseSpellEffect: ScriptableObject
{
    public abstract void Apply(Vector3 castPoint, CastTarget target, Action<CastTarget> onEffectFinish);

    public abstract void InitResources();
}