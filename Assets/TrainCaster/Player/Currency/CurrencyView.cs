using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    private const float DurationUpdate = 1.8f;

    [SerializeField] private TMP_Text _label;
    [SerializeField] private Image _icon;
    [SerializeField] private RectTransform _currencyStorage;

    private Currency _currency;
    private Tween _currentTween;
    private int _countOnTable;

    public Vector3 Position => _currencyStorage.position;

    public void Connect(Currency currency)
    {
        if (_currency != null)
            return;

        _currency = currency;
        _icon.sprite = currency.Icon;
        UpdateView(0);
    }

    public void ApplyNewCount(int count)
    {
        if(_currentTween != null && _currentTween.IsActive())
            _currentTween.Kill();

        UpdateView(count);
    }

    private void UpdateView(int count)
    {
        _currentTween = DOVirtual.Int(_countOnTable, count, DurationUpdate, value=>
        {
            _countOnTable = value;
            _label.text = _countOnTable.ToString();
        }).SetEase(Ease.Linear);
    }
}