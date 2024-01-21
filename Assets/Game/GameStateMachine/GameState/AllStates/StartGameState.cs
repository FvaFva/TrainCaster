using System.Collections;

public class StartGameState : BaseGameState
{
    public StartGameState(BaseGameState loader):base(stateOnEndAction: loader) { }

    protected override IEnumerable ActionSpecific()
    {
        yield return null;
    }
}