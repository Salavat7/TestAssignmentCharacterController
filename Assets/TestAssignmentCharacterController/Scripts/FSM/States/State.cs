using System;

namespace FsmScripts.States
{
    public abstract class State
    {
        public event Action Entered;
        public event Action Exited;
        protected readonly Fsm _fsm;

        public State(Fsm fsm)
        {
            _fsm = fsm;
        }

        public virtual void Enter() 
        {
            Entered?.Invoke();
        }

        public virtual void Exit()
        {
            Exited?.Invoke();
        }
        
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
    }
}