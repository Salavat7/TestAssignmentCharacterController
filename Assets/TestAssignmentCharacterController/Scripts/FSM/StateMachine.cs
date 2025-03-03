using System;
using System.Collections.Generic;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        private StateNode _current;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if(transition != null)
                ChangeState(transition.To);

            _current.State?.Update();
        }

        public void FixedUpdate()
        {
            _current.State?.FixedUpdate();
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.Enter();
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            var transition = new Transition(to, condition);
            _anyTransitions.Add(transition);
        }

        private void ChangeState(IState state)
        {
            if(state == _current.State)
                return;
            
            var previousState = _current.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.Exit();
            nextState?.Enter();
            _current = _nodes[state.GetType()];
        }

        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if(transition.Condition.Evaluate())
                    return transition;

            foreach(var transition in _current.Transitions)
                if(transition.Condition.Evaluate())
                    return transition;

            return null;
        }

        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if(node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                var transition = new Transition(to, condition);
                Transition transition1 = new(to, condition);
                Transitions.Add(transition);
            }
        }
    }
}