using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StorageCell 
{
    private List<IStored> _items = new List<IStored>();
    private readonly Transform _group;
    private readonly Func<GameObject, GameObject> _instantiateClone;

    public StorageCell(Transform group, GameObject baseItem, Func<GameObject, GameObject> instantiateClone)
    {
        _group = group;
        BaseItem = baseItem;
        _instantiateClone = instantiateClone;
    }

    public GameObject BaseItem { get; private set; }

    public void InitClones(int count)
    {
        for (int i = 0; i < count; i++)
            InitClone();
    }

    public IStored GetFree()
    {
        var freeClones = _items.Where(item => item.IsFree);
        if (freeClones.Any())
        {
            IStored clone = freeClones.First();
            return clone;
        }
        else
            return InitClone();
    }

    private IStored InitClone()
    {
        GameObject temp = _instantiateClone(BaseItem);
        temp.transform.parent = _group;
        temp.SetActive(false);
        IStored clone = temp.GetComponent<IStored>();
        _items.Add(clone);
        return clone;
    }
}