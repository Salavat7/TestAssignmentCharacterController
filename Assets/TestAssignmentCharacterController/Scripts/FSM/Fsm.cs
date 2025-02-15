using System;
using System.Collections.Generic;
using FsmScripts.States;

namespace FsmScripts
{
    public class Fsm
    {
        private Dictionary<Type, State> _states;
        private State _currentState;

        public Fsm()
        {
            _states = new Dictionary<Type, State>();
        }

        public void AddState(State state)
        {
            if (_states.TryAdd(state.GetType(), state) == false)
            {
                throw new Exception("You're trying add state of type already existing in FSM");
            }
        }

        public void SetState<T>() where T : State
        {
            var type = typeof(T);

            if (_currentState != null && _currentState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                throw new Exception($"FSM haven't {type} state");
            }
        }

        public State GetState<T>() where T : State
        {
            if (!_states.TryGetValue(typeof(T), out State state))
                throw new ArgumentException($"There is no state of {typeof(T)} in state machine");

            return state;
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }
    }
}