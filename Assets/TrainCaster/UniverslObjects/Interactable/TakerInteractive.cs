using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class TakerInteractive : MonoBehaviour
{
    private const float HitRayDistance = 100;

    [SerializeField] private LayerMask _interactableFilter;
    [SerializeField] private PotentGameZone _craftZone;
    [SerializeField] private PotentGameZone _zoomPosition;
    [SerializeField] private OutLiner _outliner;

    [Inject] private UserInput _input;

    private Interactable _currentTaken;
    private Interactable _currentHighlighted;
    private Camera _camera; 
    private RaycastHit _hit;
    private bool _isCursorOverUI;

    public event Action<Interactable> TakenChanged;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Craft.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        _input.Craft.Interact.performed -= OnInteract;
    }

    public void Change(Interactable interactable)
    {
        if (_currentTaken != null)
        {
            _currentTaken.MoveTo(_craftZone.GetRandomPoint());
            _currentTaken.ReturnedToPool -= OnTakenReturnedToPool;
        }

        _currentTaken = interactable;

        if(_currentTaken != null)
        {
            _currentTaken.MoveTo(_zoomPosition.Center, _zoomPosition.Transform);
            _currentTaken.ReturnedToPool += OnTakenReturnedToPool;
        }

        TakenChanged?.Invoke(_currentTaken);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_isCursorOverUI || _currentTaken == _currentHighlighted)
            return;

        Change(_currentHighlighted);
    }

    private void OnTakenReturnedToPool(IStored interactable)
    {
        if((IStored)_currentTaken == interactable)
        {
            Change(null);
        }
    }

    private void ChangeCurrentHighlighted(Interactable newInteractable)
    {
        if(_currentHighlighted != null)
        {
            _currentHighlighted.ChangeHighlight(false);
            _outliner.Hide();
        }

        _currentHighlighted = newInteractable;

        if (_currentHighlighted != null)
        {
            _currentHighlighted.ChangeHighlight(true);
            _outliner.Show(_currentHighlighted?.Renderer);
        }
    }

    private void Update()
    {
        _isCursorOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    private void FixedUpdate()
    {
        if (_isCursorOverUI == false)
        {
            Ray tapRay = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(tapRay, out _hit, HitRayDistance, _interactableFilter) && _hit.transform.TryGetComponent(out Interactable temp))
            {
                if (temp != _currentHighlighted)
                    ChangeCurrentHighlighted(temp);
            }
            else if (_currentHighlighted != null)
            {
                ChangeCurrentHighlighted(null);
            }
        }
    }
}