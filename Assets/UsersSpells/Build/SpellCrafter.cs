using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class SpellCrafter : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableFilter;
    [SerializeField] private PotentGameZone _cameraLoop;
    [SerializeField] private PotentGameZone _craftZone;

    [Inject] private UserInput _input;

    private Interactable _currentLooped;
    private LootBox _lootBox;
    private Interactable _currentHighlighted;
    private Camera _camera;
    private RaycastHit _hit;

    public event Action<LootBox> LootBoxChanged;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Craft.Interact.performed += OnChoose;
    }

    public ISpellBuild Craft()
    {
        return new CraftedSpell();
    }

    private void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray tapRay = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(tapRay, out _hit, 100, _interactableFilter) && _hit.transform.TryGetComponent(out Interactable temp))
            {
                if(temp != _currentHighlighted)
                    ChangeCurrent(temp);
            }
            else if(_currentHighlighted != null)
            {
                ChangeCurrent(null);
            }
        }
    }

    private void ChangeCurrent(Interactable newInteractable)
    {
        _currentHighlighted?.ChangeHighlight(false);
        _currentHighlighted = newInteractable;
        _currentHighlighted?.ChangeHighlight(true);
    }

    private void OnChoose(InputAction.CallbackContext context)
    {
        if(_currentLooped != null)
        {
            _currentLooped.MoveTo(_craftZone.GetRandomPoint());
            _currentLooped = null;
            return;
        }

        if (_currentHighlighted == null)
            return;

        _currentHighlighted.MoveTo(_cameraLoop.Center);
        _currentLooped = _currentHighlighted;
        UpdateLootBox();
    }

    private void UpdateLootBox()
    {
        LootBox newLootBox = null;

        if(_currentLooped != null && _currentLooped is LootBox)
            newLootBox = _currentLooped as LootBox;

        if(newLootBox != _lootBox)
            LootBoxChanged?.Invoke(newLootBox);

        _lootBox = newLootBox;
    }
}