using UnityEngine;

public class SpellView : CardView
{
    [Header("Spell view settings")]
    [SerializeField] SpellSlot _slot;

    private void OnEnable()
    {
        _slot.UpdatedSpell += OnSpellChanged;
    }

    private void OnDisable()
    {
        _slot.UpdatedSpell -= OnSpellChanged;
    }

    private void OnSpellChanged(ISpellBuild spell)
    {
        SetContent(spell);
    }

    protected override void MainButtonCollBack(){ }
}
