using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CurrencyService : MonoBehaviour, IInitialized
{
    private const int CountTransportPreLoad = 10;

    private Dictionary<CurrencyMiner, Currency> _miners;
    private List<Currency> _currencies;
    private bool _isInited;

    [Inject] private PoolService _poolService;
    [Inject] private GameStateBuilder _loader;

    public event Action<int, Currency> Mined;
    public event Action<Currency> Added;

    private void Awake()
    {
        _miners = new Dictionary<CurrencyMiner, Currency>();
        _currencies = new List<Currency>();
        _loader.EnterToLoadPool(this);
    }

    private void OnEnable()
    {
        foreach (CurrencyMiner miner in _miners.Keys)
            miner.Mined += OnMined;
    }

    private void OnDisable()
    {
        foreach (CurrencyMiner miner in _miners.Keys)
            miner.Mined += OnMined;
    }

    public void Init()
    {
        _isInited = true;

        foreach (Currency currency in _currencies)
            _poolService.Put(currency.Transport, CountTransportPreLoad, tag = currency.Name);
    }

    public void ConnectMiner(CurrencyMiner miner, Currency currency)
    {
        _miners.Add(miner, currency);
        miner.Mined += OnMined;

        if(_currencies.Contains(currency) == false)
        {
            _currencies.Add(currency);

            if(_isInited)
                _poolService.Put(currency.Transport, CountTransportPreLoad, currency.Name);

            Added?.Invoke(currency);
        }
    }

    private void OnMined(int count, Vector3 position, CurrencyMiner miner)
    {
        Currency currency = _miners[miner];
        MinedCurrencyTransport transport = _poolService.Resolve<MinedCurrencyTransport>(currency.Name);
        transport.Arrived += OnArrived;
        transport.Move(count, position, currency);
    }

    private void OnArrived(int count, MinedCurrencyTransport transport, Currency currency)
    {
        transport.Arrived -= OnArrived;
        Mined?.Invoke(count, currency);
    }
}