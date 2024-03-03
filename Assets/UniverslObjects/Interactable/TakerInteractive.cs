using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class TakerInteractive : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableFilter;
    [SerializeField] private PotentGameZone _craftZone;
    [SerializeField] private PotentGameZone _cameraLoop;

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

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_isCursorOverUI)
            return;

        Interactable taken = null;

        if (_currentTaken != null)
            _currentTaken.MoveTo(_craftZone.GetRandomPoint());

        if (_currentHighlighted != null)
        {
            _currentHighlighted.MoveTo(_cameraLoop.Center);
            taken = _currentHighlighted;
        }

        _currentTaken = taken;
        TakenChanged?.Invoke(_currentTaken);
    }

    private void ChangeCurrent(Interactable newInteractable)
    {
        _currentHighlighted?.ChangeHighlight(false);
        _currentHighlighted = newInteractable;
        _currentHighlighted?.ChangeHighlight(true);
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

            if (Physics.Raycast(tapRay, out _hit, 100, _interactableFilter) && _hit.transform.TryGetComponent(out Interactable temp))
            {
                if (temp != _currentHighlighted)
                    ChangeCurrent(temp);
            }
            else if (_currentHighlighted != null)
            {
                ChangeCurrent(null);
            }
        }
    }
}