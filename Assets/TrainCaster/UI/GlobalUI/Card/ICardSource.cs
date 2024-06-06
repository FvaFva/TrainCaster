using UnityEngine;

public interface ICardSource
{
    public string Description { get; }
    public string Name { get; }
    public Sprite Icon { get; }
}