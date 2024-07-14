using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class InventoryView : MonoBehaviour, IInitialized
{
    [SerializeField] private InventoryCellView _prefab;
    [SerializeField][Range(0, 500)] private int _countPreload;
    [SerializeField] private Transform _contentArea;

    [Inject] private GameStateBuilder _game;
    [Inject] private DiContainer _container;

    private IInventory _inventory;
    private List<InventoryCellView> _slots;

    private void Awake()
    {
        _slots = new List<InventoryCellView>();
    }

    private void OnEnable()
    {
        if (_inventory != null)
            _inventory.Changed += OnInventoryChange;
    }

    private void OnDisable()
    {
        if (_inventory != null)
            _inventory.Changed -= OnInventoryChange;
    }

    public void ConnectInventory(IInventory inventory)
    {
        if (_inventory != null)
            return;

        if (inventory == null)
            return;

        _inventory = inventory;
        _inventory.Changed += OnInventoryChange;
        _game.EnterToLoadPool(this);
    }

    public void Init()
    { 
        for(int i  = 0; i < _countPreload; i++)
        {
            InventoryCellView view = _container.InstantiatePrefab(_prefab.gameObject, _contentArea).GetComponent<InventoryCellView>();
            _slots.Add(view);
            view.Activated += OnSlotActivate;
        }
    }

    private void OnInventoryChange()
    {
        int countInInventory = 0;

        foreach (ICard part in _inventory.Parts)
        {
            _slots[countInInventory].SetContent(part);
            countInInventory++;
        }

        for(int j = countInInventory; j < _countPreload; j++)
            _slots[j].SetContent(null); ;
    }

    private void OnSlotActivate(ICard part)
    {
        _inventory.Choose(part);
    }
}