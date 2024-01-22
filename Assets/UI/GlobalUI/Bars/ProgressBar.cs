using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class ProgressBar: MonoBehaviour
{
    [SerializeField] private TMP_Text _textPanel;
    [SerializeField] private Slider _progressBar;

    private IValueSource _source;

    private void OnEnable()
    {
        UpdateObserve(true);
    }

    private void OnDisable()
    {
        UpdateObserve(false);
    }

    public void SetSource(IValueSource source)
    {
        UpdateObserve(false);
        _source = source;
        UpdateObserve(true);
        ChangeProgressBar();

        if (_textPanel != null)
            ChangeProgressText();
    }

    private void ChangeProgressBar()
    {
        _progressBar.value = Mathf.Clamp01(_source.Current / _source.Max);
    }

    private void ChangeProgressText()
    {
        _textPanel.text = $"{_source.Current} / {_source.Max}";
    }

    private void UpdateObserve(bool isObserving)
    {
        if(_source != null)
        {
            if (isObserving)
            {
                _source.Changed += ChangeProgressBar;
                if (_textPanel != null)
                    _source.Changed += ChangeProgressText;
            }
            else
            {
                _source.Changed -= ChangeProgressBar;
                if (_textPanel != null)
                    _source.Changed -= ChangeProgressText;

            }
        }
    }
}