using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private BaseGameState _currentState;
    private Coroutine _currentAction;

    private void Awake()
    {
        EnterState(GameStates.Instance.GetState<StartGameState>());
    }

    private void EnterState(BaseGameState state)
    {
        LeaveCurrentState();
        Debug.Log($"enter state {state}");
        if (state != null)
            _currentState = state;
        else
            return;

        _currentState.NewStateFound += EnterState;
        _currentAction = StartCoroutine(_currentState.Action());
    }

    private void LeaveCurrentState()
    {
        if (_currentAction != null)
            StopCoroutine(_currentAction);

        if (_currentState != null)
            _currentState.NewStateFound -= EnterState;

        _currentState = null;
    }
}