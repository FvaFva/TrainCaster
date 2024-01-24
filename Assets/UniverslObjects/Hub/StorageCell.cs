using System;
using System.Collections.Generic;
using UnityEngine;

public class StorageCell<PrefabType>: ICell<PrefabType> where PrefabType : IStored
{
    private Queue<PrefabType> _items = new Queue<PrefabType>();

    public StorageCell(Transform parent, PrefabType prefab) 
    {
        Parent = parent;
        Prefab = prefab;
    }

    public PrefabType Prefab { get; private set; }
    public Transform Parent { get; private set; }

    public bool TryGetFree(out IStored item)
    {
        bool isHave = _items.TryDequeue(out PrefabType temp);
        item = temp;
        return isHave;
    }

    public void AddItem(IStored item)
    {
        _items.Enqueue((PrefabType)item);
    }
}