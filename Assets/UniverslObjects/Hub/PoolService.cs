﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolService: MonoBehaviour
{
    private Dictionary<GameObject, ICell<IStored>> _cells = new Dictionary<GameObject, ICell<IStored>>();
    private Transform _transform;

    [Inject] private DiContainer _container;

    private void Awake()
    {
        _transform = transform;
    }

    public void Put<StoredType>(GameObject prefab, int count) where StoredType : IStored
    {
        Put<StoredType>(prefab, _transform, count);
    }

    public void Put<StoredType>(GameObject prefab, Transform parent, int count) where StoredType : IStored
    {
        if(prefab.TryGetComponent<StoredType>(out StoredType storedPrefab))
        {
            ICell<IStored> cell;

            if (_cells.ContainsKey(prefab))
            {
                cell = _cells[prefab];
            }
            else
            {
                cell = (ICell<IStored>)new StorageCell<StoredType>(parent, storedPrefab);
                _cells.Add(prefab, cell);
            }

            for (int i = 0; i < count; i++)
            {
                cell.AddItem(InitClone<StoredType>(cell));
            }
        }
    }

    public StoredType Get<StoredType>(GameObject prefab) where StoredType : IStored
    {
        bool isHaveItem = _cells.ContainsKey(prefab);
        StoredType clone = default(StoredType);

        if (isHaveItem)
        {
            IStored freeClone;

            if (_cells[prefab].TryGetFree(out freeClone))
                clone = (StoredType)freeClone;
            else
                clone = InitClone<StoredType>(_cells[prefab]);
        }

        return clone;
    }

    private StoredType InitClone<StoredType>(ICell<IStored> cell) where StoredType : IStored
    {
        GameObject clone = _container.InstantiatePrefab(cell.Prefab.gameObject, cell.Parent);
        clone.name = $"{cell.Prefab.gameObject.name} ({typeof(StoredType)})";
        clone.SetActive(false);
        clone.TryGetComponent<StoredType>(out StoredType temp);
        temp.ConnectToCell(cell);
        return temp;
    }
}