
public abstract class FSMState
{
    protected readonly FSM Fsm;
    protected readonly PlayerDragger PlayerDragger;

    public FSMState(FSM fsm,PlayerDragger playerDragger)
    {
        Fsm = fsm;
        PlayerDragger = playerDragger;
    }
    
    public virtual void Enter() { }
    public virtual void UpdatePosition() { }
    public virtual void Exit() { }
}
