using System.Collections;

public class MainGameState : BaseGameState
{
    public MainGameState(BaseGameState finishState) : base(finishState)
    {
    }

    protected override IEnumerable ActionSpecific()
    {
        while (true)
        {
            yield return null;
        }
    }
}