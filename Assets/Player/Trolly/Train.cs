using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Train : MonoBehaviour
{
    private const int MinPosition = 0;

    [SerializeField] private float _positionStep;
    [SerializeField] private float _speed;
    [SerializeField] private float _startPosition;
    [SerializeField] private List<Wagon> _wagons;

    [Inject] private UserInput _input;

    private Transform _transform;
    private int _currentPosition = MinPosition;
    private int _maxPosition;

    public event Action<Wagon, Wagon> ChoseNewWagons;

    private void Awake()
    {
        _maxPosition = _wagons.Count;
        _transform = transform;
    }

    private void OnEnable()
    {
        _input.Movement.ChangeWagon.performed += ChangePosition;
        MoveTrain();
    }

    private void OnDisable()
    {
        _input.Movement.ChangeWagon.performed -= ChangePosition;
    }

    private void ChangePosition(InputAction.CallbackContext context)
    {
        int newPosition = Mathf.Clamp(_currentPosition + (int)context.ReadValue<Single>(), MinPosition, _maxPosition);

        if(newPosition != _currentPosition)
        {
            _currentPosition = newPosition;
            MoveTrain();
        }
    }

    private void UpdateWagons()
    {
        if(_currentPosition == MinPosition)
            ChoseNewWagons?.Invoke(_wagons[MinPosition], null);
        else if(_currentPosition == _maxPosition)
            ChoseNewWagons?.Invoke(null, _wagons[_maxPosition - 1]);
        else
            ChoseNewWagons?.Invoke(_wagons[_currentPosition], _wagons[_currentPosition-1]);
    }

    private void MoveTrain()
    {
        ChoseNewWagons?.Invoke(null, null);
        _transform.DOLocalMoveX(_startPosition + _positionStep * _currentPosition, _speed).OnComplete(UpdateWagons);
    }
}
