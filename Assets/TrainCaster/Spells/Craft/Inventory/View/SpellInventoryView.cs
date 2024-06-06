using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpellInventoryView : MonoBehaviour
{
    [SerializeField] private SpellPartView _prefab;
    [SerializeField][Range(0, 500)] private int _countPreload;
    [SerializeField] private Transform _contentArea;

    [Inject] private SpellInventory _inventory;

    private List<SpellPartView> _slots;

    public void Instantiate()
    {
        if (_slots != null)
            return;

        _inventory.Changed += OnInventoryChange;
        _slots = new List<SpellPartView>();

        for(int i  = 0; i < _countPreload; i++)
        {
            SpellPartView view = Instantiate(_prefab, _contentArea);
            _slots.Add(view);
            view.Activated += OnSlotActivate;
        }
    }

    private void OnInventoryChange()
    {
        int i = 0;

        foreach(ISpellElement part in _inventory.Parts)
        {
            _slots[i].Show(part);
            i++;
        }

        for(int j = i ; j < _countPreload; j++)
            _slots[j].Show(null); ;
    }

    private void OnSlotActivate(ISpellElement part)
    {
        _inventory.Choose(part);
    }
}