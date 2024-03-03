using DG.Tweening;
using UnityEngine;

public class CanvasHider : MonoBehaviour
{
    private const float FadeTime = 1.3f;

    [SerializeField] private CanvasGroup _main;
    [SerializeField] private Canvas _canvas;

    public void Hide()
    {
        if(_canvas.isActiveAndEnabled)
            _main.DOFade(0, FadeTime).OnComplete(OnHideComplete);
    }

    public void Show()
    {
        if (_canvas.isActiveAndEnabled)
            return;

        _canvas.enabled = true;
        _main.DOFade(1, FadeTime);
    }

    private void OnHideComplete()
    {
        _canvas.enabled = false;
    }
}
