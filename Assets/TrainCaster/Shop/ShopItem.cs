using System;

[Serializable]
public struct ShopItem
{
    public int Cost;
    public Currency Currency;
    public Interactable Item;
    public string Tag => $"SHOP_{Item.Name}";
}