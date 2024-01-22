using System.Linq;
using UnityEngine;

public class EnemySelector : BaseSelector
{
    private GameObject _target;

    protected override CastTarget GetCurrentTarget()
    {
        CastTarget result;

        if (_target != null)
            result = new CastTarget(_target, true);
        else
            result = new CastTarget(PointToMove, false);

        Clear();
        return result;
    }

    protected override void ProcessSelection()
    {
        if(TryGetCameraHits(out RaycastHit[] hits))
        {
            var targetingHits = GetTypedHits<EnemyRouter>(hits); 
            IsMoving = targetingHits.Count() > 0;
            IsCorrect = IsMoving;
            _target = targetingHits.FirstOrDefault().transform?.gameObject;

            if (IsCorrect)
                PointToMove = _target.transform.position;
        }
        else
        {
            Clear();
        }
    }

    private void Clear()
    {
        _target = null;
        IsMoving = false;
        IsCorrect = false;
        PointToMove = Vector3.zero;
    }
}
