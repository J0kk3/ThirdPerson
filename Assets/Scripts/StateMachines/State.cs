
public abstract class State
{
    //Any state have to have Enter, Tick & Exit methods
    public abstract void Enter();

    public abstract void Tick(float deltaTime);

    public abstract void Exit();
}