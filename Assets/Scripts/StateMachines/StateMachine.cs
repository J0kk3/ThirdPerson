using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    State currentState;
    void Update()
    {
        //the ? here checks if its not null
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        newState?.Enter();
    }
}