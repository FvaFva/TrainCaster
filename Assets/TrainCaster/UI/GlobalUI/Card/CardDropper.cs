using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CardDropper : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryCellView _cell;

    [Inject] private DragContainer _dragView;

    public void OnDrop(PointerEventData eventData)
    {
        if (_cell.IsWithContent)
            _dragView.Hide();
        else
            ApplyNewContent();
    }

    private void ApplyNewContent()
    {
        _cell.SetContent(_dragView.Content);
        _dragView.Finish();
    }
}