
namespace FSMContent
{
    public abstract class FSMState
    {
        protected readonly FSM Fsm;
        protected readonly PlayerDragger PlayerDragger;

        protected FSMState(FSM fsm,PlayerDragger playerDragger)
        {
            Fsm = fsm;
            PlayerDragger = playerDragger;
        }
        
        public virtual void UpdateState() { }
    }
}