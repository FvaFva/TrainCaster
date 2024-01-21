using System.Collections.Generic;
using UnityEngine;

public class Storage: MonoBehaviour
{
    private Dictionary<GameObject, StorageCell> _cells = new Dictionary<GameObject, StorageCell>();

    public static Storage Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Put(GameObject product, int count)
    {
        if(product.TryGetComponent(out IStored temp))
        {
            GetStorageCell(product).InitClones(count);
        }
        else
        {
            Debug.Log($"stored unstorabel {product}");
        }
    }

    public bool TryGetFree<Type>(GameObject product, out Type clone) where Type : IStored
    {
        bool isHaveItem = _cells.ContainsKey(product);

        if (isHaveItem)
            clone = (Type)_cells[product].GetFree();
        else
            clone = default(Type);

        return isHaveItem;
    }

    private StorageCell GetStorageCell(GameObject product)
    {
        if(_cells.ContainsKey(product))
        {
            return _cells[product];
        }
        else
        {
            Transform group = new GameObject(product.name).transform;
            group.SetParent(transform);
            StorageCell newCell = new StorageCell(group, product, InitClone);
            _cells.Add(product, newCell);
            return newCell;
        }
    }

    private GameObject InitClone(GameObject BaseItem)
    {
        return Instantiate(BaseItem);
    }
}