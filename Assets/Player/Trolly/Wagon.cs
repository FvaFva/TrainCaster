using UnityEngine;
using Zenject;
using DG.Tweening;

public class Wagon:MonoBehaviour
{
    [SerializeField] private Transform _castPoint;
    [SerializeField] private SpellBuild _test;

    [Inject] private GameStateBuilder _state;

    private Spell _spell;
    private Transform _transform;
    private Sequence _mover;

    public Transform CastPoint => _castPoint;
    public SpellBuild Spell => _test;

    private void Awake()
    {
        _state.EnterToLoadPool(_test);
        _spell = new Spell(_test);
        _transform = transform;
        _mover = DOTween.Sequence();
    }

    public void Cast(CastTarget target)
    {
        _spell.Cast(_castPoint.position, target);
    }

    public void MoveToPoint(PotentGameZone nextPoint, float moveTime)
    {
        _mover.Append(_transform.DOLocalMove(nextPoint.Center, moveTime)).Join(_transform.DOLocalRotate(nextPoint.Direction, moveTime));
    }
}