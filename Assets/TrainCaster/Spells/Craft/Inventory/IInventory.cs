using System;
using System.Collections.Generic;

public interface IInventory
{
    public event Action Changed;
    public IEnumerable<ICard> Parts { get; }
    public void Choose(ICard part);
}