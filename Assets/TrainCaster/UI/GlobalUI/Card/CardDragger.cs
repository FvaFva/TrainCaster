using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private InventoryCellView _cell;

    [Inject] private DragContainer _dragView;

    private void Awake()
    {
        if (_cell == null)
            Debug.LogError($"Achtung! CardDragger without elements {gameObject}!");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_cell.IsWithContent == false)
            return;

        _cell.HideContent();
        _dragView.StartDrag(_cell.Content);
        _dragView.ReturnedBack += OnDragComeBack;
        _dragView.Finished += OnDragFinished;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    private void OnDragComeBack()
    {
        _cell.ShowContent();
        Unsubscribe();
    }

    private void OnDragFinished()
    {
        _cell.SetContent(null);
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _dragView.ReturnedBack -= OnDragComeBack;
        _dragView.Finished -= OnDragFinished;
    }
}
