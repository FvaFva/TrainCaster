using System;
using UnityEngine;
using Zenject;

public abstract class CurrencyMiner : MonoBehaviour
{
    private const int MaxChance = 100;
    private const int MinChance = 0;

    [SerializeField] private Currency _currency;
    [SerializeField, Range(MinChance, MaxChance)] private int _chance;

    private int _dice;
    private int _basic;

    [Inject] private CurrencyService _currencyMineService;

    public event Action<int, Vector3, CurrencyMiner> Mined;

    private void Awake()
    {
        _currencyMineService.ConnectMiner(this, _currency);
    }

    private void OnEnable()
    {
        EnableImpact();
    }

    private void OnDisable()
    {
        DisableImpact();
    }

    protected abstract void EnableImpact();
    protected abstract void DisableImpact();

    protected void TriggerMine(Vector3 minePosition)
    {
        if(UnityEngine.Random.Range(MinChance, MaxChance) < _chance)
            Mined?.Invoke(_basic + UnityEngine.Random.Range(0, _dice), minePosition, this);
    }

    protected void UpdateMineValues(int basic, int dice)
    {
        _basic = basic;
        _dice = dice;
    }
}
