using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class OpenLootBox : MonoBehaviour
{
    [SerializeField] private SpellCrafter _crafter;
    [SerializeField] private Canvas _canvas;
    
    private Button _button;

    private void Awake()
    {
        if(TryGetComponent(out _button) == false)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _crafter.LootBoxChanged += OnBoxChange;
        _button.onClick.AddListener(OnActivate);
    }

    private void OnDisable()
    {
        _crafter.LootBoxChanged -= OnBoxChange;
        _button.onClick.RemoveListener(OnActivate);
    }

    private void OnBoxChange(LootBox lootBox)
    {
        _canvas.enabled = lootBox != null;
    }

    private void OnActivate()
    {
        _crafter.OpenBox();
    }
}
