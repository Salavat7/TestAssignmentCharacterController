using UnityEngine;

namespace FsmScripts.States
{
    public class IdleState : State
    {
        private readonly IInput _iInput;
        private Vector2 _inputVector;
        private bool _jump;

        public IdleState(Fsm fsm, IInput iInput) : base(fsm) 
        {
            _iInput = iInput;
            // _inputVector = Vector2.zero;
            _jump = false;
        }

        public override void Update()
        {
            if(_inputVector != Vector2.zero)
            {
                Debug.Log(_inputVector.x);
                Debug.Log(_inputVector.y);
                _fsm.SetState<RunState>();
            }

            if(_jump)
            {
                _fsm.SetState<JumpState>();
            }
        }

        public override void Enter()
        {
            base.Enter();
            _iInput.Moved += OnMoved;
            _iInput.Jumped += OnJumped;
        }

        public override void Exit()
        {
            base.Exit();
            _iInput.Moved -= OnMoved;
            _iInput.Jumped -= OnJumped;
        }

        private void OnMoved(Vector2 vector)
        {
            _inputVector = vector;
        }

        private void OnJumped(bool jump)
        {
            _jump = jump;
        }
    }
}