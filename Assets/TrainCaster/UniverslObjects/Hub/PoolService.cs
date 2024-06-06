using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolService: MonoBehaviour
{
    private Dictionary<(string, Type), Pool> _pools = new Dictionary<(string, Type), Pool>();
    private Transform _transform;

    [Inject] private DiContainer _container;

    private void Awake()
    {
        _transform = transform;
    }

    public void Put<StoredType>(StoredType origin, int count, string tag = null) where StoredType : IStored => Put(origin, _transform, count, tag);

    public void Put<StoredType>(StoredType origin, Transform parent, int count, string tag = null) where StoredType : IStored
    {
        var key = (tag, typeof(StoredType));
        Pool pool;

        if (_pools.ContainsKey(key))
        {
            pool = _pools[key];
        }
        else
        {
            pool = new Pool(parent, origin);
            _pools.Add(key, pool);
        }

        for (int i = 0; i < count; i++)
            pool.AddItem(InitClone(origin, parent, tag));
    }

    public StoredType Resolve<StoredType>() where StoredType : IStored => Resolve<StoredType>(null);

    public StoredType Resolve<StoredType>(string tag) where StoredType : IStored
    {
        var key = (tag, typeof(StoredType));
        Pool pool = _pools[key];

        if (pool.IsEmpty())
            pool.AddItem(InitClone((StoredType)pool.Prefab, pool.Parent, tag));

        return (StoredType)pool.GetFree();
    }

    private StoredType InitClone<StoredType>(StoredType origin, Transform parent, string tag) where StoredType : IStored
    {
        GameObject clone = _container.InstantiatePrefab(origin.gameObject, parent);
        clone.name = $"{origin.gameObject.name} ({typeof(StoredType)} - {tag})";
        clone.SetActive(false);
        clone.TryGetComponent(out StoredType temp);
        return temp;
    }
}