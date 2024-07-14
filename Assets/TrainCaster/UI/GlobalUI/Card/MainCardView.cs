using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCardView : MonoBehaviour, ICardView
{
    private const float TimeToDissolve = 3.5f;
    private const float TimeToHide = 0.25f;
    private const string TextureName = "_baseMap";
    private const string UVName = "_BaseMapUV";

    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Renderer _icon;
    [SerializeField] private Image _rarity;
    [SerializeField] private Dissolver _dissolver;
    [SerializeField] private Button _close;
    [SerializeField] private List<RarityColorMapElement> _colors;

    private ICard _current;
    private Texture _baseIcon;
    private Vector4 _baseUV;
    private Transform _baseTransform;
    private bool _isVisible;
    private Coroutine _imageChanger;

    public event Action Chose;
    public event Action Opened;

    private void Awake()
    {
        _baseIcon = _icon.material.GetTexture(TextureName);
        _baseUV = _icon.material.GetVector(UVName);
        _baseTransform = transform;
        _dissolver.Show(0.1f);
    }

    private void OnEnable()
    {
        _close.AddListener(Close);
    }

    private void OnDisable()
    {
        _close.RemoveListener(Close);
    }

    public void SetSource(ICard card)
    {
        if(_current == card) 
            return;

        _current = card;

        if (card != null)
        {
            _icon.material.SetTexture(TextureName, card.Icon.texture);
            _icon.material.SetVector(UVName, card.Icon.GetUV());
            _description.text = card.Description;
            _name.text = card.Name;

            foreach (RarityColorMapElement element in _colors)
            {
                if (element.Rarity == _current.Rarity)
                {
                    _rarity.color = element.Color;
                    break;
                }
            }

            if (_isVisible == false)
            {
                _isVisible = true;
                gameObject.SetActive(true);
                _baseTransform.DOScale(1, TimeToHide).OnComplete(() =>_dissolver.Hide(TimeToDissolve));
                Opened?.Invoke();
            }
            else
            {
                if(_imageChanger !=  null)
                    StopCoroutine( _imageChanger);

                _imageChanger = StartCoroutine(ChangeImage());
            }
        }
        else if(_isVisible)
        {
            Close();
            _icon.material.SetTexture(TextureName, _baseIcon);
            _icon.material.SetVector(UVName, _baseUV); 
            _dissolver.Show(TimeToHide);
        }
    }

    private void Close()
    {
        if (_isVisible == false)
            return;

        _isVisible = false;
        _baseTransform.DOScale(0, TimeToHide).OnComplete(() => { gameObject.SetActive(false); });
    }

    private IEnumerator ChangeImage()
    {
        _dissolver.Show(TimeToHide);
        yield return new WaitForSeconds(TimeToDissolve);
        _dissolver.Hide(TimeToDissolve);
    }
}