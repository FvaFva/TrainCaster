using System;
using UnityEngine;

public abstract class BaseBullet: MonoBehaviour, IStored
{
    [SerializeField] private float _speed = 5f;

    private Transform _transform;

    protected Vector3 Position => _transform.position;
    protected float Speed => _speed;

    public event Action<CastTarget, BaseBullet> Crushed;
    public event Action<IStored> ReturnedToPool;

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

    public void Shot(Vector3 position, CastTarget target)
    {
        _transform.position = position;
        gameObject.SetActive(true);
        ShotImpact(position, target);
    }

    protected abstract void ShotImpact(Vector3 position, CastTarget target);
    protected abstract Vector3 CalculateNextStep(float deltaTime);
    protected abstract bool IsCanFly();
    protected abstract bool IsItCorrectTarget(GameObject stoper);

    private void Finish(CastTarget target)
    {
        Crushed?.Invoke(target, this);
        ReturnedToPool?.Invoke(this);
        gameObject.SetActive(false);
    }
}
