using UnityEngine;
using Zenject;
using DG.Tweening;
using System;

public class Wagon:MonoBehaviour
{
    [SerializeField] private Transform _castPoint;
    [SerializeField] private SpellBuild _test;

    [Inject] private GameStateBuilder _state;

    private Spell _spell;
    private Transform _transform;

    public Transform CastPoint => _castPoint;
    public ISpellBuild Spell { get; private set; }
    public event Action Arrived;
    public event Action SpellChanged;

    private void Awake()
    {
        ApplySpellBuild(_test);
        _transform = transform;
    }

    public void ApplySpellBuild(ISpellBuild spell)
    {
        Spell = spell;
        _state.EnterToLoadPool(spell);
        _spell = new Spell(spell);
        SpellChanged?.Invoke();
    }

    public void Cast(CastTarget target)
    {
        _spell.Cast(_castPoint.position, target);
    }

    public void MoveToPoint(PotentGameZone nextPoint, float moveTime, Ease ease)
    {
        DOTween.Sequence().Append(_transform.DOMove(nextPoint.Center, moveTime).OnComplete(OnArrived)).Join(_transform.DOLocalRotateQuaternion(nextPoint.Direction, moveTime)).SetEase(ease).Play();
    }

    private void OnArrived()
    {
        Arrived?.Invoke();
    }
}