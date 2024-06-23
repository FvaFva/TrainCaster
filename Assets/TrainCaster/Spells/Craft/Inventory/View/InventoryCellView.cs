using System;

public class InventoryCellView : CardView
{
    private ICard _current;
    public event Action<ICard> Activated;

    public override void SetSource(ICard source)
    {
        _current = source;
        base.SetSource(_current);
    }

    protected override void MainButtonCollBack()
    {
        if (_current != null)
            Activated?.Invoke(_current);
    }

    protected override void AwakeCallBack()
    {
        SetSource(null);
    }
}