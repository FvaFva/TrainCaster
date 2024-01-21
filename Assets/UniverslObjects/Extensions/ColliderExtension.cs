using System;
using UnityEngine;

public static class ColliderExtension
{
    public static bool IsItPlayerBuild(this Collider collider )
    {
        return collider.TryGetComponent<PlayerBuild>(out PlayerBuild temp);
    }

    public static bool IsSamePoint<T>(this Collider collider, T point) where T : MonoBehaviour
    {
        return collider.TryGetComponent(out T colliderPoint) && colliderPoint == point;
    }

    public static Collider ContainBehavior<Search>(this Collider collider, ref bool isContain) where Search : MonoBehaviour
    {
        isContain |= collider.TryGetComponent(out Search colliderPoint);
        return collider;
    }

    public static bool ContainBehavior<Search>(this Collider collider) where Search : MonoBehaviour
    {
        return collider.TryGetComponent(out Search colliderPoint);
    }
}

