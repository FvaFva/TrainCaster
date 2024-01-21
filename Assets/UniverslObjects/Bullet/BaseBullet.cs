using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBullet: MonoBehaviour, IStored
{
    public abstract bool IsFree { get; }
    public abstract void Shot(Vector3 position, Vector3 direction, Action<IEnumerable<CastTarget>> onCrush);

    protected Action<IEnumerable<CastTarget>> _onCrush;
    protected Type TargetType;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.IsItPlayerBuild())
            return;

        CastTarget stoper = new CastTarget(collision.gameObject, true);
        Finish(stoper);
    }

    protected void Finish(CastTarget target)
    {
        _onCrush?.Invoke(new CastTarget[] {target});
        gameObject.SetActive(false);
    }
}
