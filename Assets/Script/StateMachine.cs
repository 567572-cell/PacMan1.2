// Holds the current state and swaps between states.
public class StateMachine
{
    public IState Current { get; private set; }

    public void ChangeState(IState next)
    {
        Current?.Exit();   // leave the old state (if there is one)
        Current = next;
        Current.Enter();   // start the new state
    }

    public void Tick()
    {
        Current?.Tick();
    }
}
