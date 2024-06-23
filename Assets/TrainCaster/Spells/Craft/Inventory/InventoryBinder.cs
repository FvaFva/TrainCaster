using UnityEngine;
using Zenject;

public class InventoryBinder : MonoBehaviour, IInitialized
{
    
    [SerializeField] private InventoryView _spellElementView;
    [SerializeField] private InventoryView _craftedSpellView;
    [SerializeField] private LootBoxUnpacker _boxUnpacker;

    [Inject] private Inventory<SpellElement> _elements;
    [Inject] private Inventory<CraftedSpell> _spells;
    [Inject] private GameStateBuilder _game;

    public void Init()
    {
        _boxUnpacker.TEMP_LoadPreset();
    }

    private void Awake()
    {
        _spellElementView.ConnectInventory(_elements);
        _craftedSpellView.ConnectInventory(_spells);
        _game.EnterToLoadPool(this);
    }
}