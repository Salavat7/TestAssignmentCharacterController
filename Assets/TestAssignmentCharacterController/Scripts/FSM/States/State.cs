namespace FsmScripts.States
{
    public abstract class State
    {
        protected readonly Fsm _fsm;

        public State(Fsm fsm)
        {
            _fsm = fsm;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}