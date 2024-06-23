using System;
using System.Collections.Generic;

public class Inventory<T> : IInventory where T : ICard
{
    private IInventorySource _source;
    private List<T> _elements = new List<T>();

    public Inventory(IInventorySource unpacker)
    {
        _source = unpacker;
        _source.Mined += AddCard;
    }

    public IEnumerable<ICard> Parts => (IEnumerable<ICard>)_elements;

    public event Action Changed;
    public event Action<T> Chose;

    public void Remove(T part)
    {
        if (_elements.Contains(part))
        {
            _elements.Remove(part);
            Changed?.Invoke();
        }
    }

    public void Choose(ICard card)
    {
        if (card is T element)
        {
            if (_elements.Contains(element))
                Chose?.Invoke(element);
        }
    }

    private void AddCard(ICard card)
    {
        if (card is T element) 
            Add(element);
    }

    private void Add(T newElement)
    {
        _elements.Add(newElement);
        Changed?.Invoke();
    }
}