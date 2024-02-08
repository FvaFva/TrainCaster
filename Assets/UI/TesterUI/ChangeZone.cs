using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeZone : MonoBehaviour
{
    [SerializeField] private SpellCrafter _crafter;
    [SerializeField] private SpellCaster _caster;

    private Button _button;
    private GameObject _current;
    private Camera _camera;
    private Queue<GameObject> _next;

    private void Awake()
    {
        TryGetComponent<Button>(out _button);
        _camera = Camera.main;
        _current = _caster.gameObject;
        _next = new Queue<GameObject>();
        _crafter.gameObject.SetActive(false);
        _next.Enqueue(_crafter.gameObject);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Change);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Change);
    }

    private void Change()
    {
        _next.Enqueue(_current);
        _current.SetActive(false);
        _current = _next.Dequeue();
        _current.SetActive(true);
        _camera.transform.Rotate(70, 180, 0);
    }
}
