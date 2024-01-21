using System.Collections;
using System.Collections.Generic;

public class LoaderGameState : BaseGameState
{
    private List<IInitialized> _initializedPool = new List<IInitialized>();
    private bool _initialized = false;

    public LoaderGameState(BaseGameState gamePlayState):base(stateOnEndAction: gamePlayState) {}

    public void Add(IInitialized item)
    {
        _initializedPool.Add(item);

        if(_initialized)
            item.Init();
    }

    protected override IEnumerable ActionSpecific()
    {
        foreach (var item in _initializedPool)
        {
            item.Init();
            yield return null;
        }

        _initialized = true;
    }
}