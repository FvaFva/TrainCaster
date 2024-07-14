using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
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

    protected Sprite DefaultIcon {  get; private set; }
    public bool IsWithContent { get; private set; }

    public event Action Chose;

    private void Awake()
    {
        _descriptionElement.gameObject.SetActive(false);
        _transform = transform as RectTransform;
        _delayFollow = new WaitForSeconds(DelayFollowSeconds);
        _deltaFollow = new Vector2(0.3f, 0);
        DefaultIcon = _icon.sprite;
        AwakeCallBack();
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

    public virtual void SetContent(ICard content)
    {
        IsWithContent = content != null;
        _label.enabled = IsWithContent;
        _description.enabled = IsWithContent;

        if (IsWithContent)
        {
            _icon.sprite = content.Icon;
            _label.text = content.Name;
            _description.text = content.Description;
            _isHaveDescription = content.Description != string.Empty;
        }
        else
        {
            _icon.sprite = DefaultIcon;
            _label.text = string.Empty;
            _description.text = string.Empty;
            _isHaveDescription = false;
        }
    }

    protected abstract void MainButtonCollBack();
    protected virtual void AwakeCallBack(){ }

    protected void UpdateIcon(Sprite icon)
    {
        if(icon != null)
        {
            _icon.sprite = icon;
        }
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

    private void OnClickMainButton()
    {
        MainButtonCollBack();
        Chose?.Invoke();
    }
}