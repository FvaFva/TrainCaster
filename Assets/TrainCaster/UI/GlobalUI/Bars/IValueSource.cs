using System;

public interface IValueSource
{
    public event Action Changed;
    public float Max { get; }
    public float Current { get; }
}
