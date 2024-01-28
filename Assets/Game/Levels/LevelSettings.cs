using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private PotentGameZone _near1;
    [SerializeField] private PotentGameZone _near2;
    [SerializeField] private PotentGameZone _middle1;
    [SerializeField] private PotentGameZone _middle2;
    [SerializeField] private PotentGameZone _far1;
    [SerializeField] private PotentGameZone _far2;

    [SerializeField] private GameObject _zonePrefab;

    private List<PotentGameZone> _zones;

    private void Awake()
    {
        _zones = new List<PotentGameZone>() { _near1, _near2, _middle1, _middle2, _far1, _far2 };

        foreach (PotentGameZone zone in _zones)
            Instantiate(gameObject, zone.transform).transform.localScale = new Vector3(zone.Size.x, 1, zone.Size.y);
    }
}
