// Every ghost state must have these three methods.
// Enter runs once when the state starts, Tick every frame, Exit once when it ends.
public interface IState
{
    void Enter();
    void Tick();
    void Exit();
}
