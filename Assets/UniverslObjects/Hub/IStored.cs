using System;
using UnityEngine;

public interface IStored
{
    public void ConnectToCell(ICell<IStored> myCell);
    public GameObject gameObject { get; }
}