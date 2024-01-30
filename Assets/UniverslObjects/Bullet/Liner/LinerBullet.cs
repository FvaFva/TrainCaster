using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinerBullet : BaseBullet
{
    [SerializeField] private float _speed;
    [SerializeField] private int _distance;

    private Coroutine _fly;

    public override void Shot(Vector3 position, Vector3 direction, Action<CastTarget> onCrush)
    {
        transform.position = position;
        _onCrush = onCrush;
        gameObject.SetActive(true);

        if (_fly != null)
            StopCoroutine(_fly);

        _fly = StartCoroutine(Flying(direction));
    }

    private IEnumerator Flying(Vector3 direction)
    {
        yield return null;

        for(float  i = 0; i < _distance; i += Time.deltaTime * _speed)
        {
            transform.position += _speed * Time.deltaTime * direction;
            yield return null;
        }

        Finish(new CastTarget(transform.position, false));
    }
}
