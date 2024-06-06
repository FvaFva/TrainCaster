using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxUnpacker : MonoBehaviour
{
    [SerializeField] private TakerInteractive _taker;
    [SerializeField] private Button _open;
    [SerializeField] private List<SpellPart> _tempTest;

    private LootBox _current;

    public event Action<SpellPart, SpellPartRarities> OnOpen;
    public event Action<LootBox> OnChange;

    public void TEMP_LoadPreset()
    {
        foreach(SpellPart part in _tempTest)
            OnOpen?.Invoke(part, SpellPartRarities.Common);
    }

    private void OnEnable()
    {
        _taker.TakenChanged += TryTakeLootBox;
        _open.onClick.AddListener(Open);
    }

    private void OnDisable()
    {
        _taker.TakenChanged -= TryTakeLootBox;
        _open.onClick.RemoveListener(Open);
    }

    private void TryTakeLootBox(Interactable _interactable)
    {
        LootBox newBox = _interactable as LootBox;

        if (newBox != _current)
        {
            _current = newBox;
            OnChange?.Invoke(newBox);
        }
    }

    private void Open()
    {
        if(_current != null)
        {
            OnOpen?.Invoke(_current.Open(), SpellPartRarities.Legendary);
        }
    }
}