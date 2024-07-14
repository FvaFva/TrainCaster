using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView: CardView
{
    [Header("Shop item view settings")]
    [SerializeField] private TMP_Text _cost;
    [SerializeField] private Image _currencyIcon;

    private ShopItem _shopItem;

    public event Action<ShopItem> Bought;

    public void SetSource(ShopItem item)
    {
        if (_shopItem.Item != null)
            return;

        _shopItem = item;
        _cost.text = item.Cost.ToString();
        _currencyIcon.sprite = item.Currency.Icon;
        base.SetContent(_shopItem.Item);
    }

    protected override void MainButtonCollBack()
    {
        Bought?.Invoke(_shopItem);
    }
}