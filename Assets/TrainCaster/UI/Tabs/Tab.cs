using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Tab
{
    [SerializeField] private Image _buttonHighlight;
    [SerializeField] private Button _button;
    [SerializeField] private Canvas _canvas;

    public event Action<Tab> Activated;

    public void ChangeVision(bool isVision)
    {
        _buttonHighlight.enabled = isVision;
        _canvas.enabled = isVision;
    }

    public void Enable()
    {
        _button.enabled = true;
        _button.onClick.AddListener(OnClick);
    }

    public void Disable()
    {
        _button.enabled = false;
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Activated?.Invoke(this);
    }
}