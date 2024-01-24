using System;
using UnityEngine;

public interface IStored
{
    public event Action OnTurnOff;
    public GameObject gameObject { get; }
}