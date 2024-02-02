using System;
using UnityEngine;

public abstract class BaseBullet: MonoBehaviour, IStored
{
    [SerializeField] private float _speed = 5f;

    private ICell<BaseBullet> _cell;
    private Action<CastTarget> _onCrush;
    private Transform _transform;

    protected Vector3 Position => _transform.position;
    protected float Speed => _speed;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _transform.position += CalculateNextStep(Time.deltaTime);

        if(IsCanFly() == false)
            Finish(new CastTarget(_transform.position, false));
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.IsItPlayerBuild())
            return;

        if (IsItCorrectTarget(collision.gameObject) == false)
            return;

        CastTarget stoper = new CastTarget(collision.gameObject, true);
        Finish(stoper);
    }

    public void Shot(Vector3 position, CastTarget target, Action<CastTarget> onCrush)
    {
        _onCrush = onCrush;
        _transform.position = position;
        gameObject.SetActive(true);
        ShotImpact(position, target);
    }

    public void ConnectToCell(ICell<IStored> myCell)
    {
        _cell = (ICell<BaseBullet>)myCell;
    }

    protected abstract void ShotImpact(Vector3 position, CastTarget target);
    protected abstract Vector3 CalculateNextStep(float deltaTime);
    protected abstract bool IsCanFly();
    protected abstract bool IsItCorrectTarget(GameObject stoper);

    private void Finish(CastTarget target)
    {
        _onCrush?.Invoke(target);
        _cell.AddItem(this);
        gameObject.SetActive(false);
    }
}
