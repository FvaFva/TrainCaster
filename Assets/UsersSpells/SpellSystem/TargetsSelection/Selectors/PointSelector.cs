using System.Linq;
using UnityEngine;

public class PointSelector : BaseSelector
{
    protected override CastTarget GetCurrentTarget()
    {
        return new CastTarget(PointToMove, IsCorrect);
    }

    protected override void ProcessSelection()
    {
        if (TryGetCameraHits(out RaycastHit[] hits))
        {
            var movingHits = GetTypedHits<Place>(hits);
            IsMoving = movingHits.Count() > 0;
            PointToMove = movingHits.FirstOrDefault().point;
            IsCorrect = true;
        }
        else
        {
            IsMoving = false;
            IsCorrect = false;
        }
    }
}