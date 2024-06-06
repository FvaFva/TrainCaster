using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Railways : MonoBehaviour, IInitialized
{
    [SerializeField] private List<PotentGameZone> _allPoints;

    [Inject] private GameStateBuilder _state;

    private Dictionary<PotentGameZone, PotentGameZone> _nextPoints;
    private Dictionary<PotentGameZone, PotentGameZone> _previousPoints;

    private void OnValidate()
    {
        for (int i = 0; i < _allPoints.Count; i++)
            _allPoints[i].Rename($"Way position {i}");
    }

    private void Awake()
    {
        _nextPoints = new Dictionary<PotentGameZone, PotentGameZone> ();
        _previousPoints = new Dictionary<PotentGameZone, PotentGameZone>();

        if (_allPoints.Count == 0)
            return;
        
        if (_allPoints.Count == 1)
        {
            _nextPoints.Add(_allPoints[0], _allPoints[0]);
            _previousPoints.Add(_allPoints[0], _allPoints[0]);
            return;
        }
        
        int lastPosition = _allPoints.Count - 1;

        for (int i =0;  i < lastPosition; i++)
            _nextPoints.Add(_allPoints[i], _allPoints[i + 1]);

        _nextPoints.Add(_allPoints[lastPosition], _allPoints[0]);

        foreach(KeyValuePair<PotentGameZone, PotentGameZone> point in _nextPoints)
        {
            _previousPoints.Add(point.Value, point.Key);
        }

        _state.EnterToLoadPool(this);
    }

    public PotentGameZone GetNextPoint(PotentGameZone currentPoint)
    {
        if(_nextPoints.ContainsKey(currentPoint))
            return _nextPoints[currentPoint];
        else return null;
    }

    public PotentGameZone GetPreviousPoint(PotentGameZone currentPoint)
    {
        if (_previousPoints.ContainsKey(currentPoint))
            return _previousPoints[currentPoint];
        else return null;
    }

    public void Init()
    {
        foreach (KeyValuePair<PotentGameZone, PotentGameZone> point in _nextPoints)
            point.Key.LookAt(point.Value.Center);
    }

    public IEnumerable<PotentGameZone> GetWayTo(PotentGameZone start, PotentGameZone goal)
    {
        List<PotentGameZone> way = new List<PotentGameZone>();

        PotentGameZone current = GetNextPoint(start);

        while(current != null && current != goal)
        {
            way.Add(current);
            current = GetNextPoint(current);
        }

        way.Add(goal);

        return way;
    }
}