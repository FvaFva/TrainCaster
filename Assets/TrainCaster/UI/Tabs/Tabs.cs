using System.Collections.Generic;
using UnityEngine;

public class Tabs : MonoBehaviour
{
    [SerializeField] private List<Tab> _tabs;

    private Tab _current;

    private void Awake()
    {
        _current = _tabs[0];
        _current.ChangeVision(true);

        for (int i = 1; i < _tabs.Count; i++)
            _tabs[i].ChangeVision(false);
    }

    private void OnEnable()
    {
        foreach (Tab tab in _tabs)
        {
            tab.Enable();
            tab.Activated += OnChose;
        }
    }

    private void OnDisable()
    {
        foreach (Tab tab in _tabs)
        {
            tab.Disable();
            tab.Activated -= OnChose;
        }
    }

    private void OnChose(Tab choseTab)
    {
        _current.ChangeVision(false);
        _current = choseTab;
        _current.ChangeVision(true);
    }
}
