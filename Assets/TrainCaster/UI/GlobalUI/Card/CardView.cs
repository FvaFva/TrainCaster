using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float DelayFollowSeconds = 0.02f;

    [Header("Card view settings")]
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private RectTransform _descriptionElement;
    [SerializeField] private Button _mainButton;

    private Vector2 _deltaFollow;
    private bool _isPointInside;
    private RectTransform _transform;
    private WaitForSeconds _delayFollow;
    private bool _isHaveDescription;
    private Sprite _defaultIcon;

    public event Action Chose;

    private void Awake()
    {
        _descriptionElement.gameObject.SetActive(false);
        _transform = transform as RectTransform;
        _delayFollow = new WaitForSeconds(DelayFollowSeconds);
        _deltaFollow = new Vector2(0.3f, 0);
        _defaultIcon = _icon.sprite;
    }

    private void OnEnable()
    {
        _mainButton.onClick.AddListener(OnClickMainButton);
        _isPointInside = false;
    }

    private void OnDisable()
    {
        _mainButton.onClick.RemoveListener(OnClickMainButton);
    }

    protected void SetSource(ICardSource source)
    {
        if (source != null)
        {
            _icon.sprite = source.Icon;
            _label.text = source.Name;
            _description.text = source.Description;
            _isHaveDescription = source.Description != string.Empty;
        }
        else
        {
            _icon.sprite = _defaultIcon;
            _label.text = string.Empty;
            _description.text = string.Empty;
            _isHaveDescription = false;
        }
    }

    private void OnClickMainButton()
    {
        MainButtonCollBack();
        Chose?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isHaveDescription == false)
            return;

        _isPointInside = true;
        StartCoroutine(FollowCursor());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointInside = false;
    }

    private IEnumerator FollowCursor()
    {
        _descriptionElement.gameObject.SetActive(true);

        while (_isPointInside)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_transform, Input.mousePosition, null, out Vector2 cursorPosition);
            yield return _delayFollow;
            _descriptionElement.localPosition = cursorPosition + _deltaFollow;
        }

        _descriptionElement.gameObject.SetActive(false);
    }

    protected abstract void MainButtonCollBack();
}