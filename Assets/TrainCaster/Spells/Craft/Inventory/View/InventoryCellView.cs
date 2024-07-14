using System;
using UnityEngine;

public class InventoryCellView : CardView
{
    public event Action<ICard> Activated;

    public ICard Content { get; private set; }

    public override void SetContent(ICard content)
    {
        Content = content;
        base.SetContent(Content);
    }

    public void HideContent()
    {
        UpdateIcon(DefaultIcon);
    }

    public void ShowContent()
    {
        UpdateIcon(Content.Icon);
    }

    protected override void MainButtonCollBack()
    {
        if (Content != null)
            Activated?.Invoke(Content);
    }

    protected override void AwakeCallBack()
    {
        SetContent(null);
    }
}