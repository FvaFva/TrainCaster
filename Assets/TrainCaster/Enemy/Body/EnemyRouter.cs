using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyBody))]
public class EnemyRouter : MonoBehaviour, IStored
{
    private EnemyView _model;
    private EnemyBody _body;
    private EnemyMover _mover;
    private Transform _transform;
    private EnemyStatusBar _statusBar;
    private List<IEnemyPart> _parts;

    public Vector3 Position => _transform.position;
    public event Action<EnemyView> ViewChanged;
    public event Action<EnemyRouter, EnemyDeleteStatus> Deleted;
    public event Action<IStored> ReturnedToPool;

    private void Awake()
    {
        bool haveAllParts = TryGetComponent<EnemyBody>(out EnemyBody tempBody);
        haveAllParts = TryGetComponent<EnemyMover>(out EnemyMover tempMover) && haveAllParts;

        if (haveAllParts == false)
           Destroy(this);

        _mover = tempMover;
        _body = tempBody;

        _parts = new List<IEnemyPart>() { _mover, _body };
        _transform = transform;
        _statusBar = new EnemyStatusBar();
    }

    private void OnEnable()
    {
        foreach (var part in _parts)
            part.Completed += Delete;

        _statusBar.StaticChanged += OnStatusStaticChange;
        _statusBar.HitPointsTick += ApplyDamage;
    }

    private void OnDisable()
    {
        foreach (var part in _parts)
            part.Completed -= Delete;

        _statusBar.StaticChanged -= OnStatusStaticChange;
        _statusBar.HitPointsTick -= ApplyDamage;
    }

    private void Update()
    {
        _statusBar.UpdateChanges(Time.deltaTime);
    }

    public void StartPath(EnemyPath path)
    {
        _mover.StartPath(path);
    }

    public void ApplyDamage(float  damage)
    {
        _body.ApplyDamage(damage);
    }

    public void ApplyStatus(EnemyStatusParameters status)
    {
        _statusBar.ApplyStatus(status);
    }

    public void Activate(EnemyView model)
    {
        gameObject.SetActive(true);
        _model = model;
        _model.Activate(transform);
        ViewChanged?.Invoke(model);

        foreach (var part in _parts)
            part.ImplementModel(model);
    }

    private void Delete(EnemyDeleteStatus status)
    {
        _model.TakeOff();
        Deleted?.Invoke(this, status);
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        _model = null;
        ReturnedToPool?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnStatusStaticChange()
    {
        foreach (var part in _parts)
            part.ImplementStatus(_statusBar.SummaryChanges);

        _model.ChangeAnimationSpeed(_statusBar.SummaryChanges.MoveSpeed);
    }
}
