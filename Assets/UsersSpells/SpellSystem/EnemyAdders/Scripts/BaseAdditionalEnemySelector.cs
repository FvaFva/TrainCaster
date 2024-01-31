using UnityEngine;
using Zenject;

public abstract class BaseAdditionalEnemySelector:ScriptableObject
{
    [Inject] protected ActiveEnemies Enemies {  get; private set; }
    public abstract CastTarget ProcessCastTarget(CastTarget target, int count = 0, float radius = 0);
}