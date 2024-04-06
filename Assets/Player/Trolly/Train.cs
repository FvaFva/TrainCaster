using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Train : MonoBehaviour, IInitialized
{
    private const int FirstWagon = 0;

    [SerializeField] private PotentGameZone _firstPosition;
    [SerializeField] private Railways _railways;
    [SerializeField] private List<Wagon> _wagons;
    [SerializeField] private float _moveTime;

    [Inject] private UserInput _input;
    [Inject] private GameStateBuilder _state;

    private PotentGameZone _currentPosition;
    private int _currentWagon;
    private int _lastWagon;

    public event Action<Wagon, Wagon> ChoseNewWagons;

    private void Awake()
    {
        _lastWagon = _wagons.Count;
        _currentPosition = _firstPosition;
        _state.EnterToLoadPool(this);
    }
    private void OnEnable()
    {
        _input.Movement.ChangeWagon.performed += ChangePosition;
    }

    private void OnDisable()
    {
        _input.Movement.ChangeWagon.performed -= ChangePosition;
    }

    private void ChangePosition(InputAction.CallbackContext context)
    {
        int newPosition = Mathf.Clamp(_currentWagon + (int)context.ReadValue<Single>(), FirstWagon, _lastWagon);

        if (newPosition == _currentWagon)
            return;
        else if(newPosition > _currentWagon)
            _currentPosition = _railways.GetNextPoint(_currentPosition);
        else
            _currentPosition = _railways.GetPreviousPoint(_currentPosition);

        _currentWagon = newPosition;
        MoveWagons();
        UpdateWagons();
    }

    private void UpdateWagons()
    {
        if(_currentWagon == FirstWagon)
            ChoseNewWagons?.Invoke(_wagons[FirstWagon], null);
        else if(_currentWagon == _lastWagon)
            ChoseNewWagons?.Invoke(null, _wagons[_lastWagon - 1]);
        else
            ChoseNewWagons?.Invoke(_wagons[_currentWagon], _wagons[_currentWagon-1]);
    }

    private void MoveWagons()
    {
        ChoseNewWagons?.Invoke(null, null);
        PotentGameZone previousPosition = _currentPosition;
        _wagons[0].MoveToPoint(_currentPosition, _moveTime);

        for (int i = 1; i < _lastWagon; i++)
        {
            previousPosition = _railways.GetPreviousPoint(previousPosition);
            _wagons[i].MoveToPoint(previousPosition, _moveTime);
        }
    }

    public void Init()
    {
        MoveWagons();
        UpdateWagons();
    }
}
