using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyBody))]
public class EnemyRouter : MonoBehaviour
{
    private EnemyModel _model;
    private EnemyBody _body;
    private EnemyMover _mover;
    private List<IEnemyPart> _parts;

    public event Action PathFinished;

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
            part.TakeOff += TakeOff;
    }

    private void OnDisable()
    {
        foreach (var part in _parts)
            part.TakeOff -= TakeOff;
    }

    public void StartPath(EnemyPath path)
    {
        _mover.StartPath(path);
    }

    public void ApplyDamage(float  damage)
    {
        _body.ApplyDamage(damage);
    }

    public void InitModel(EnemyModel model)
    {
        _model = model;
        _model.Initialized(this);

        foreach (var part in _parts)
        {
            part.ImplementModel(model);
        }
    }

    private void TakeOff()
    {
        _model.TakeOff();
        _model = null;
        PathFinished?.Invoke();
        gameObject.SetActive(false);
    }
}
