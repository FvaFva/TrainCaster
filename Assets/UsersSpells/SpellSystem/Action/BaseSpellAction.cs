using UnityEngine;

public abstract class BaseSpellAction : ScriptableObject
{
    public abstract void Apply(CastTarget target);
}