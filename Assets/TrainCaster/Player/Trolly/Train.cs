using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Train : MonoBehaviour, IInitialized
{
    private const int FirstWagon = 0;
    private const float TimeToChangePosition = 1.3f;
    private const float TimeToChangeZone = 0.25f;

    [SerializeField] private PotentGameZone _firstPosition;
    [SerializeField] private Railways _railways;
    [SerializeField] private List<Wagon> _wagons;

    [Inject] private UserInput _input;
    [Inject] private GameStateBuilder _state;

    private PotentGameZone _currentPosition;
    private int _currentWagon;
    private int _lastWagon;
    private bool _isMovable;
    private float _moveTime;
    private Queue<PotentGameZone> _cashedWay;

    public event Action<Wagon, Wagon> ChoseNewWagons;

    private void Awake()
    {
        _cashedWay = new Queue<PotentGameZone>();
        _lastWagon = _wagons.Count;
        _currentPosition = _firstPosition;
        _state.EnterToLoadPool(this);
        _moveTime = TimeToChangePosition;
    }

    private void OnEnable()
    {
        _input.Movement.ChangeWagon.performed += ChangePosition;
        _wagons[FirstWagon].Arrived += OnWagonArrived;

        foreach (Wagon wagon in _wagons)
            wagon.SpellChanged += UpdateWagons;
    }

    private void OnDisable()
    {
        _input.Movement.ChangeWagon.performed -= ChangePosition;
        _wagons[FirstWagon].Arrived -= OnWagonArrived;

        foreach (Wagon wagon in _wagons)
            wagon.SpellChanged -= UpdateWagons;
    }

    public void MoveToPosition(PotentGameZone goal)
    {
         foreach(PotentGameZone newPosition in _railways.GetWayTo(_currentPosition, goal))
            _cashedWay.Enqueue(newPosition);

        _currentWagon = 0;
        _moveTime = TimeToChangeZone;

         if (_isMovable)
            MoveWagons(Ease.InSine);
    }

    public void Init()
    {
        MoveWagons(Ease.Linear);
        UpdateWagons();
    }

    private void ChangePosition(InputAction.CallbackContext context)
    {
        if (_isMovable == false)
            return;

        int newPosition = Mathf.Clamp(_currentWagon + (int)context.ReadValue<Single>(), FirstWagon, _lastWagon);

        if (newPosition == _currentWagon)
            return;
        else if(newPosition > _currentWagon)
            _currentPosition = _railways.GetNextPoint(_currentPosition);
        else
            _currentPosition = _railways.GetPreviousPoint(_currentPosition);

        _currentWagon = newPosition;
        MoveWagons(Ease.InOutCirc);
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

    private void MoveWagons(Ease ease)
    {
        ChoseNewWagons?.Invoke(null, null);
        PotentGameZone previousPosition = _currentPosition;
        _wagons[0].MoveToPoint(_currentPosition, _moveTime, ease);
        _isMovable = false;

        for (int i = 1; i < _lastWagon; i++)
        {
            previousPosition = _railways.GetPreviousPoint(previousPosition);
            _wagons[i].MoveToPoint(previousPosition, _moveTime, ease);
        }
    }

    private void OnWagonArrived()
    {
        if(_cashedWay.Count > 0)
        {
            _currentPosition = _cashedWay.Dequeue();

            if(_cashedWay.Count == 0)
            {
                MoveWagons(Ease.OutBack);
                _moveTime = TimeToChangePosition;
            }
            else
            {
                MoveWagons(Ease.Linear);
            }
        }
        else
        {
            _isMovable = true;
            UpdateWagons();
        }
    }
}
