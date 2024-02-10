using UnityEngine;
using DG.Tweening;

public abstract class Interactable : MonoBehaviour
{
    private const float MoveDuration = 0.4f;

    [Header("Interactable settings")]
    [SerializeField] private ParticleSystem _highlightEffect;

    private Transform _transform;

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

    public void MoveTo(Vector3 newPosition)
    {
        _transform.DOMove(newPosition, MoveDuration);
    }

    protected abstract void AwakeLoad();
}
