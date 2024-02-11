using System;
using UnityEngine;
using UnityEngine.UI;

public class LootBoxUnpacker : MonoBehaviour
{
    [SerializeField] private TakerInteractive _taker;
    [SerializeField] private Button _open;

    private LootBox _current;

    public event Action<SpellPart> OnOpen;
    public event Action<LootBox> OnChange;

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
            OnOpen?.Invoke(_current.Open());
        }
    }
}