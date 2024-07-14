using UnityEngine;

public class ZoomViewer: MonoBehaviour
{
    [SerializeField] private MainCardView _mainCardView;
    [SerializeField] private TakerInteractive _takerInteractive;

    private void OnEnable()
    {
        _mainCardView.Opened += OnMainCardViewOpen;
        _takerInteractive.TakenChanged += OnTakerInteract;
    }

    private void OnDisable()
    {
        _mainCardView.Opened -= OnMainCardViewOpen;
        _takerInteractive.TakenChanged -= OnTakerInteract;
    }

    private void OnMainCardViewOpen()
    {
        _takerInteractive.Change(null);
    }

    private void OnTakerInteract(Interactable interactable)
    {
        if(interactable != null)
            _mainCardView.SetSource(null);
    }
}