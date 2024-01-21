using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Caster : MonoBehaviour
{
    [SerializeField] EnemySelector _enemySelector;
    [SerializeField] PointSelector _pointSelector;
    [SerializeField] StaticSelector _staticSelector;
    [SerializeField] SpellBar _spellBar;

    private UserInput _input;
    private BaseSelector _currentSelector;
    private Dictionary<TypesSelection, BaseSelector> _selectionMap;
    private Guid _caster;

    private void Awake()
    {
        _selectionMap = new Dictionary<TypesSelection, BaseSelector>
        {
            { TypesSelection.Enemy, _enemySelector },
            { TypesSelection.Point, _pointSelector },
            { TypesSelection.Vector, _staticSelector }
        };

        _input = new UserInput();

        _input.Cast.CastSkillA.performed += StartSelectTarget;
        _input.Cast.CastSkillA.canceled += TargetSelected;
        _spellBar.BindSlotToAction(_input.Cast.CastSkillA.id);

        _input.Cast.CastSkillB.performed += StartSelectTarget;
        _input.Cast.CastSkillB.canceled += TargetSelected;
        _spellBar.BindSlotToAction(_input.Cast.CastSkillB.id);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void TargetSelected(InputAction.CallbackContext context)
    {
        if (_caster != context.action.id)
            return;

        _caster = default;
        CastTarget target = _currentSelector.Finishing();

        if (target.IsCorrect)
            _spellBar.Cast(context.action.id, target);
    }

    private void StartSelectTarget(InputAction.CallbackContext context)
    {
        if(_caster != default)
            return;

        _caster = context.action.id;

        if (_spellBar.IsActive(_caster))
        {
            _currentSelector = _selectionMap[_spellBar.GetSelector(_caster)];
            _currentSelector.SetCastPoint(_spellBar.GetCastPoint(_caster));
            _currentSelector.Starting();
        }
        else
        {
            _caster = default;
        }
    }
}
