using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DragContainer: MonoBehaviour
{
    private const float TimesMoveBack = 0.3f;
    private const string TextureName = "_MainTex";
    private const string UVName = "_BaseMapUV";

    [SerializeField] private Image _view;

    private RectTransform _myTransform;
    private Vector3 _homePosition;
    private bool _isDragging;

    public event Action ReturnedBack;
    public event Action Finished;
    public ICard Content { get; private set; }

    private void Awake()
    {
        if(transform is RectTransform rect)
            _myTransform = rect;
        else
            Debug.LogError("Drag view is not rect");

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_isDragging)
            _myTransform.position = Input.mousePosition;
    }

    public void StartDrag(ICard card)
    {
        if (card == null)
            return;

        gameObject.SetActive(true);
        _isDragging = true;
        _view.material.SetTexture(TextureName, card.Icon.texture);
        _view.material.SetVector(UVName, card.Icon.GetUV());
        _homePosition = Input.mousePosition;
    }

    public void Finish()
    {
        Content = null;
        Finished?.Invoke();
        gameObject.SetActive(false);
    }

    public void Hide()
    {
        _isDragging = false;
        Content = null;

        _myTransform.DOMove(_homePosition, TimesMoveBack).OnComplete(() =>
        {
            ReturnedBack?.Invoke();
            gameObject.SetActive(false);
        });
    }
}