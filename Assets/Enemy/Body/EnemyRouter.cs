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
    private List<IEnemyPart> _parts;
    private ICell<EnemyRouter> _cell;

    public event Action PathFinished;
    public event Action Died;

    private void Awake()
    {
        bool haveAllParts = TryGetComponent<EnemyBody>(out EnemyBody tempBody);
        haveAllParts = TryGetComponent<EnemyMover>(out EnemyMover tempMover) && haveAllParts;

        if (haveAllParts == false)
           Destroy(this);

        _mover = tempMover;
        _body = tempBody;

        _parts = new List<IEnemyPart>() { _mover, _body };
    }

    private void OnEnable()
    {
        foreach (var part in _parts)
            part.Finished += TakeOff;
    }

    private void OnDisable()
    {
        foreach (var part in _parts)
            part.Finished -= TakeOff;
    }

    public void StartPath(EnemyPath path)
    {
        _mover.StartPath(path);
    }

    public void ApplyDamage(float  damage)
    {
        _body.ApplyDamage(damage);
    }

    public void Activate(EnemyView model)
    {
        gameObject.SetActive(true);
        _model = model;
        _model.Activate(transform);

        foreach (var part in _parts)
            part.ImplementModel(model);
    }

    public void ConnectToCell(ICell<IStored> myCell)
    {
        _cell = (ICell<EnemyRouter>)myCell;
    }

    private void TakeOff(bool isDie)
    {
        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;
        _model.TakeOff();
        _model = null;
        _cell.AddItem(this);

        if (isDie)
            Died?.Invoke();
        else
            PathFinished?.Invoke();

        gameObject.SetActive(false);
    }
}
