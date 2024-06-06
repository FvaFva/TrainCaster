using UnityEngine;
using DG.Tweening;
using System;

public abstract class Interactable : MonoBehaviour, IStored, ICardSource
{
    private const float AnimationDuration = 0.4f;

    [Header("Interactable settings")]
    [SerializeField] private ParticleSystem _highlightEffect;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Transform _view;
    [SerializeField] private Vector3 _viewDelta;

    private Transform _transform;

    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;
    public bool IsActive {  get; private set; }

    public event Action<IStored> ReturnedToPool;

    private void Awake()
    {
        _transform = transform;
        AwakeLoad();
    }

    public void ChangeHighlight(bool highlight)
    {
        if (highlight)
            _highlightEffect.Play();
        else
            _highlightEffect.Stop();
    }

    public void Activate(Vector3 newPosition)
    {
        gameObject.SetActive(true);
        _view.localScale = Vector3.one;
        _view.rotation = Quaternion.identity;
        _view.localPosition = Vector3.zero;
        IsActive = true;
        MoveTo(newPosition);
    }

    public void MoveTo(Vector3 newPosition)
    {
        _transform.DOMove(newPosition + _viewDelta, AnimationDuration);
    }

    protected abstract void AwakeLoad();

    protected void Deactivate()
    {
        IsActive = false;
        DOTween.Sequence().Append(_view.DOShakeRotation(AnimationDuration, 300, 30)).Join(_view.DOScale(Vector3.zero, AnimationDuration)).OnComplete(() =>
            {
                _explosion.Play();
                DOVirtual.DelayedCall(_explosion.main.duration, OnDeactivateAnimationFinish);
            });
    }

    private void OnDeactivateAnimationFinish()
    {
        gameObject.SetActive(false);
        ReturnedToPool?.Invoke(this);
    }
}
