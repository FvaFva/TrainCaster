using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Train : MonoBehaviour
{
    private const int MinPosition = 0;

    [SerializeField] private float _positionStep;
    [SerializeField] private List<Wagon> _wagons;

    private Transform _transform;
    private UserInput _input;
    private int _currentPosition = MinPosition;
    private int _maxPosition;
    private Vector3 _startPosition;

    public event Action<Wagon, Wagon> ChoseNewWagons;

    private void Awake()
    {
        _input = new UserInput();
        _maxPosition = _wagons.Count;
        _transform = transform;
        _startPosition = _transform.position;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Movement.Moving.performed += ChangePosition;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Movement.Moving.performed -= ChangePosition;
    }

    private void ChangePosition(InputAction.CallbackContext context)
    {
        int newPosition = Mathf.Clamp(_currentPosition + (int)context.ReadValue<Single>(), MinPosition, _maxPosition);

        if(newPosition != _currentPosition)
        {
            _currentPosition = newPosition;
            UpdateWagons();
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

        _transform.position = _startPosition + Vector3.right * _positionStep * _currentPosition;
    }
}
