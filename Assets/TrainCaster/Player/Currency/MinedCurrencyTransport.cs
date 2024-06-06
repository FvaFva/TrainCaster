using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;


public class MinedCurrencyTransport : MonoBehaviour, IStored
{
    private const float MoveTime = 1.0f;
    private const float OffDelay = 2f;

    [SerializeField] private ParticleSystem _graphic;
    [SerializeField] private MeshRenderer _view;

    private Transform _transform;
    private int _count;
    private Currency _currency;
    private WaitForSeconds _delay;

    public event Action<int, MinedCurrencyTransport, Currency> Arrived;
    public event Action<IStored> ReturnedToPool;

    private void Awake()
    {
        _delay = new WaitForSeconds(OffDelay);
        _transform = transform;
        _graphic.Pause();
        gameObject.SetActive(false);
    }

    public void Move(int count, Vector3 position, Currency currency)
    {
        _view.enabled = true;
        _count = count;
        _transform.position = position;
        _currency = currency;
        _graphic.Play();
        gameObject.SetActive(true);
        _transform.DOMove(currency.StoragePosition, MoveTime).OnComplete(OnArrived);
    }

    private void OnArrived()
    {
        Arrived?.Invoke(_count, this, _currency);
        _count = 0;
        _graphic.Stop(withChildren: true, stopBehavior: ParticleSystemStopBehavior.StopEmitting);
        StartCoroutine(DelayedOff());
    }

    private IEnumerator DelayedOff()
    {
        yield return _delay;
        _currency = null;
        ReturnedToPool?.Invoke(this);
        gameObject.SetActive(false);
    }
}
