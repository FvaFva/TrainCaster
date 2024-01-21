using UnityEngine;

public class StaticSelector : BaseSelector
{
    [SerializeField] private float _distance;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _layerMAsk;

    private RaycastHit _hit;
    private Vector3 _endPoint;

    protected override CastTarget GetCurrentTarget()
    {
        return new CastTarget(_endPoint, true);
    }

    protected override void ProcessSelection()
    {
        IsCorrect = true;
        IsMoving = true;
        PointToMove = CastPoint.position;
        _lineRenderer.SetPosition(0, CastPoint.position);

        if (Physics.Raycast(CastPoint.position, CastPoint.forward, out _hit, _layerMAsk))
            _endPoint = _hit.point;
        else
            _endPoint = CastPoint.position + CastPoint.forward * _distance;

        _lineRenderer.SetPosition(1, _endPoint);
    }
}