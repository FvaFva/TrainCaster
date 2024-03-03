using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellCrafterView : MonoBehaviour
{
    [SerializeField] private SpellCrafter _crafter;
    [SerializeField] private SpellPartView _prefab;
    [SerializeField] private RectTransform _content;

    private List<SpellPartView> _spellParts;

    private void Awake()
    {
        _spellParts = new List<SpellPartView>();

        foreach (SpellPartTypes type in Enum.GetValues(typeof(SpellPartTypes)))
            _spellParts.Add(Instantiate(_prefab, _content));
    }

    private void OnEnable()
    {
        _crafter.Changed += OnPartsChanged;
    }

    private void OnDisable()
    {
        _crafter.Changed -= OnPartsChanged;
    }

    private void OnPartsChanged(IEnumerable<SpellPart> parts)
    {
        int i = 0;

        foreach (SpellPart part in parts)
        {
            _spellParts[i].Show(part);
            i++;
        }

        for (int j = i; j < _spellParts.Count; j++)
        {
            _spellParts[j].Show(null);
        }
    }
}
