using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private readonly Queue<IStored> _items = new Queue<IStored>();
    private readonly List<IStored> _inWorld = new List<IStored>();

    public Pool(Transform parent, IStored prefab) 
    {
        Parent = parent;
        Prefab = prefab;
    }

    public IStored Prefab { get; private set; }
    public Transform Parent { get; private set; }

    public bool IsEmpty()
    {
        return _items.Count == 0;
    }

    public IStored GetFree()
    {
        bool isHave = _items.TryDequeue(out IStored item);

        if (isHave)
        {
            _inWorld.Add(item);
            item.ReturnedToPool += AddItem;
        }

        return item;
    }

    public void AddItem(IStored item)
    {
        _items.Enqueue(item);

        if(_inWorld.Contains(item))
            item.ReturnedToPool -= AddItem;
    }
}