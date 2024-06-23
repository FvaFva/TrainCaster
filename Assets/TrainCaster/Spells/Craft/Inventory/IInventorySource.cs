using System;

public interface IInventorySource
{
    public event Action<ICard> Mined;
}