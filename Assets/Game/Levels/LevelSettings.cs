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

    private List<PotentGameZone> _zones;

    private void Awake()
    {
        _zones = new List<PotentGameZone>() { _near1, _near2, _middle1, _middle2, _far1, _far2 };
    }
}
