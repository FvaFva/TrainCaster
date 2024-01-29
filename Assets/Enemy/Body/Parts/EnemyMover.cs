using System;
using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour, IEnemyPart
{
    private const float SqrMagnitudeMinimumToTarget = 0.001f;
    private const float Delay = 0.01f;
    private const float RotateSpeed = 10f;

    private Vector3 _targetPoint;
    private float _speed;
    private int _currentPathPoint;
    private EnemyPath _path;
    private WaitForSeconds _sleep;
    private Coroutine _movingToPoint;
    private Transform _transform;

    public event Action<EnemyDeleteStatus> Completed;

    private void Awake()
    {
        _transform = transform;
        _sleep = new WaitForSeconds(Delay);
    }

    private void OnDisable()
    {
        if (_movingToPoint != null)
        {
            StopCoroutine(_movingToPoint);
            _movingToPoint = null;
        }
    }

    public void StartPath(EnemyPath path)
    {
        _transform.localPosition = Vector3.zero;
        _transform.localRotation = Quaternion.identity;
        _path = path;
        _currentPathPoint = 0;
        IsPassFinished();
        _movingToPoint = StartCoroutine(MoveToPoint());
    }

    public void ImplementModel(EnemyView model)
    {
        _speed = model.Speed;
    }

    private IEnumerator MoveToPoint()
    {
        yield return null;
        bool isHavePointToMove = true;

        while (isHavePointToMove)
        {
            Vector3 targetVector = _targetPoint - _transform.position;
            targetVector.y = 0;

            if (targetVector.sqrMagnitude > SqrMagnitudeMinimumToTarget)
            {
                _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(targetVector), Time.deltaTime * RotateSpeed);
                _transform.position += (targetVector.normalized * _speed) * Time.deltaTime;
            }
            else if(IsPassFinished())
            {
                Completed?.Invoke(EnemyDeleteStatus.FinishPath);
                isHavePointToMove = false;
            }

            yield return _sleep;
        }

        _movingToPoint = null;
    }

    private bool IsPassFinished()
    {
        EnemyPathPoint next = _path.TryGetPoint(_currentPathPoint);

        if(next == null)
            return true;

        _targetPoint = next.Position;
        _currentPathPoint++;

        return false;
    }
}