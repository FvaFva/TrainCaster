using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpellPartView : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionField;

    private ISpellPart _current;
    private Button _action;

    public event Action<ISpellPart> Activated;

    private void Awake()
    {
        if(TryGetComponent(out _action) == false)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _action.onClick.AddListener(OnAction);
    }

    private void OnDisable()
    {
        _action.onClick.RemoveListener(OnAction);
    }

    public void Show(ISpellPart current)
    {
        _current = current;

        _descriptionField.text = _current?.Description;
    }

    private void OnAction()
    {
        if(_current != null)
        {
            Activated?.Invoke(_current);
        }
    }
}