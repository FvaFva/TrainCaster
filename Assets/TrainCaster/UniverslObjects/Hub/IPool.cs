using UnityEngine;

public interface IPool<out PrefabType> where PrefabType : IStored
{
    public PrefabType Prefab { get; }
    public Transform Parent { get; }
    public bool TryGetFree(out IStored item);
    public void AddItem(IStored item);
}