using System;
using UnityEngine;

public interface IStored
{
    public event Action<IStored> ReturnedToPool;
    public GameObject gameObject { get; }
}