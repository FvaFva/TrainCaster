using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    private List<EnemyPathPoint> _points = new List<EnemyPathPoint>();

    private void Awake()
    {
        foreach (EnemyPathPoint point in GetComponentsInChildren<EnemyPathPoint>())
        {
            _points.Add(point);
        }
    }

    public EnemyPathPoint TryGetPoint(int pointNumber)
    {
        if (pointNumber < 0 || _points.Count <= pointNumber)
            return null;

        return _points[pointNumber];
    }
}
