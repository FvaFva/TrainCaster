using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerWallet: MonoBehaviour
{
    private Dictionary<Currency, int> _remaining;

    [Inject] private CurrencyService _currencyService;

    public event Action<Currency, int> Changed;

    private void Awake()
    {
        _remaining = new Dictionary<Currency, int>();
    }

    private void OnEnable()
    {
        _currencyService.Added += ApplyNewCurrency;
        _currencyService.Mined += OnMined;
    }

    private void OnDisable()
    {
        _currencyService.Added -= ApplyNewCurrency;
        _currencyService.Mined -= OnMined;
    }

    public bool TrySpend (Currency currency, int count)
    {
        if(count <= 0 || _remaining.ContainsKey(currency) == false || _remaining[currency] < count)
            return false;

        _remaining[currency] -= count;
        Changed?.Invoke(currency, _remaining[currency]);
        return true;
    }

    private void OnMined(int count, Currency currency)
    {
        if (_remaining.ContainsKey(currency) == false || count <= 0)
            return;

        _remaining[currency] += count;
        Changed?.Invoke(currency, _remaining[currency]);
    }

    private void ApplyNewCurrency(Currency currency) =>_remaining.Add(currency, 0);
}