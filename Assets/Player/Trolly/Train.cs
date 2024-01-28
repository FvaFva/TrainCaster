using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Train : MonoBehaviour
{
    private const int MinPosition = 0;

    [SerializeField] private PotentGameZone _firstPosition;
    [SerializeField] private PotentGameZone _secondPosition;
    [SerializeField] private List<Wagon> _wagons;
    [SerializeField] private float _moveTime;

    [Inject] private UserInput _input;

    private Transform _transform;
    private int _currentPosition = MinPosition;
    private int _maxPosition;
    private float _startPosition;
    private float _positionDelta;

    public event Action<Wagon, Wagon> ChoseNewWagons;

    private void Awake()
    {
        _maxPosition = _wagons.Count;
        _transform = transform;
        _positionDelta = _secondPosition.Center.x - _firstPosition.Center.x;
        _startPosition = _firstPosition.Center.x;
        PrepareWagons();
    }

    private void PrepareWagons()
    {
        for(int i = 0; i < _maxPosition; i++)
        {
            _wagons[i].transform.localPosition = Vector3.left * i * _positionDelta;
        }
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
        _transform.DOLocalMoveX(_startPosition + _positionDelta * _currentPosition, _moveTime).OnComplete(UpdateWagons);
    }
}
