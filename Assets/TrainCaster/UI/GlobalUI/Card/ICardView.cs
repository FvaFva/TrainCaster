using System;

public interface ICardView
{
    public void SetSource(ICard source);
    public event Action Chose;
}