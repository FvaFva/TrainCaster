using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shop: MonoBehaviour , IInitialized
{
    private const int CountPreloadItems = 5;

    [SerializeField] private RectTransform _viewParent;
    [SerializeField] private ShopItemView _viewPrefab;
    [SerializeField] private PotentGameZone _spawnZone;
    [SerializeField] private List<ShopItem> _items;

    private List<ShopItemView> _views;

    [Inject] private PoolService _poolService;
    [Inject] private GameStateBuilder _loader;
    [Inject] private PlayerWallet _playerWallet;

    private void Awake()
    {
        _views = new List<ShopItemView>();
        _loader.EnterToLoadPool(this);
    }

    private void OnEnable()
    {
        foreach (ShopItemView itemView in _views)
            itemView.Bought += OnChose;
    }

    private void OnDisable()
    {
        foreach (ShopItemView itemView in _views)
            itemView.Bought -= OnChose;
    }

    public void Init()
    {
        foreach (ShopItem item in _items)
        {
            _poolService.Put(item.Item, CountPreloadItems, item.Tag);
            ShopItemView newView = Instantiate(_viewPrefab, _viewParent);
            newView.SetItem(item);
            newView.Bought += OnChose;
            _views.Add(newView);
        }
    }

    private void OnChose(ShopItem choseItem)
    {
        if(_playerWallet.TrySpend(choseItem.Currency, choseItem.Cost))
        {
            Interactable item = _poolService.Resolve<Interactable>(choseItem.Tag);
            item.Activate(_spawnZone.GetRandomPoint());
        }
    }
}