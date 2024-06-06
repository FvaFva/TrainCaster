using System;
using UnityEngine;

public abstract class BaseSpellEffect : SpellPart
{
    public event Action<CastTarget> EffectFinished;

    public abstract void Apply(Vector3 castPoint, CastTarget target);

    public virtual void InitResources() { }

    public override SpellPartTypes Type => SpellPartTypes.Effect;

    public override void Accept(ISpellPartVisitor visitor, SpellPartRarities rarity)
    {
        visitor.Visit(this, rarity);
    }

    protected void Finished(CastTarget target)
    {
        EffectFinished?.Invoke(target);
    }
}