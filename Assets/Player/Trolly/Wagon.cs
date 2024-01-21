using UnityEngine;

public class Wagon:MonoBehaviour
{
    [SerializeField] private Transform _castPoint;
    [SerializeField] private SpellBuild _test;

    private Spell _spell;
    public Transform CastPoint => _castPoint;
    public SpellBuild Spell => _test;

    private void Awake()
    {
        GameStates.Instance.EnterToLoadPool(_test);
        _spell = new Spell(_test);
    }

    public void Cast(CastTarget target)
    {
        _spell.Cast(_castPoint.position, target);
    }
}