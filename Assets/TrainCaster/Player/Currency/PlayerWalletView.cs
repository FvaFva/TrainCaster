using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWalletView : MonoBehaviour
{
    [SerializeField] private Transform _viewParent;
    [SerializeField] private CurrencyView _viewPrefab;
    [SerializeField] private CurrencyService _currencyService;
    [SerializeField] private GridLayoutGroup _layoutGroup;
    [SerializeField] private PlayerWallet _wallet;

    private Dictionary<Currency, CurrencyView> _map;

    private void Awake()
    {
        _map = new Dictionary<Currency, CurrencyView>();
    }

    private void OnEnable()
    {
        _currencyService.Added += OnAdded;
        _wallet.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _currencyService.Added -= OnAdded;
        _wallet.Changed -= OnChanged;
    }

    private void OnAdded(Currency currency)
    {
        CurrencyView viewClone = Instantiate(_viewPrefab, _viewParent);
        viewClone.Connect(currency);
        Canvas.ForceUpdateCanvases();
        currency.InitiateStoragePosition(viewClone.Position);
        _map.Add(currency, viewClone);
    }

    private void OnChanged(Currency currency, int count)
    {
        if (_map.ContainsKey(currency))
            _map[currency].ApplyNewCount(count);
    }
}
