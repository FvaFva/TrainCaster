public class NonSelector : BaseSelector
{
    protected override CastTarget GetCurrentTarget()
    {
        return new CastTarget(CastPoint.position, true);
    }

    protected override void ProcessSelection()
    {
        PointToMove = CastPoint.position;
    }
}