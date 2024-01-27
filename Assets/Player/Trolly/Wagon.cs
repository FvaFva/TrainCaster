using UnityEngine;
using Zenject;

public class Wagon:MonoBehaviour
{
    [SerializeField] private Transform _castPoint;
    [SerializeField] private SpellBuild _test;

    [Inject] private GameState _state;

    private Spell _spell;
    public Transform CastPoint => _castPoint;
    public SpellBuild Spell => _test;

    private void Awake()
    {
        _state.EnterToLoadPool(_test);
        _spell = new Spell(_test);
    }

    public void Cast(CastTarget target)
    {
        _spell.Cast(_castPoint.position, target);
    }
}