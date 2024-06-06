using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneChanger : MonoBehaviour
{
    [SerializeField] private PlayZone _crafter;
    [SerializeField] private PlayZone _caster;
    [SerializeField] private CameraMover _camera;
    [SerializeField] private Button _button;
    [SerializeField] private Train _train;

    private PlayZone _current;
    private Queue<PlayZone> _next;

    private void Awake()
    {
        _current = _caster;
        _next = new Queue<PlayZone>();
        _next.Enqueue(_crafter);
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
        _current.Exit();
        _current = _next.Dequeue();
        _current.Enter();
        _train.MoveToPosition(_current.WayPoint);

        _camera.Rotate(new Vector3(40, 180, 0));
    }
}
