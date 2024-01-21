using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellView : MonoBehaviour
{
    [SerializeField] SpellSlot _slot;
    [SerializeField] TMP_Text _name;
    [SerializeField] Image _icon;

    private void OnEnable()
    {
        _slot.UpdatedSpell += OnSpellChanged;
    }

    private void OnDisable()
    {
        _slot.UpdatedSpell -= OnSpellChanged;
    }

    private void OnSpellChanged(SpellBuild spell)
    {
        if (spell != default)
        {
            _name.text = spell.Header;
            _icon.sprite = spell.Icon;
        }
        else
        {
            _name.text = "Non";
            _icon.sprite = null;
        }
    }
}
