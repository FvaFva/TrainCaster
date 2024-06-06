using System;

public class SpellPartView : CardView
{
    private ISpellElement _current;

    public event Action<ISpellElement> Activated;

    public void Show(ISpellElement current)
    {
        _current = current;
        SetSource(_current);
    }

    protected override void MainButtonCollBack()
    {
        if (_current != null)
        {
            Activated?.Invoke(_current);
        }
    }
}