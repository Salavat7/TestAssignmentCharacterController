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
        }

        public override void Update()
        {
            if (_inputVector != Vector2.zero)
            {
                _fsm.SetState<RunState>();
            }

            if (_jump)
            {
                _fsm.SetState<JumpState>();
            }
        }

        public override void Enter()
        {
            base.Enter();
            _iInput.Moved += OnMoved;
            _iInput.Jumped += OnJumped;
            CleanInput();
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

        private void CleanInput()
        {
            _inputVector = Vector2.zero;
            _jump = false;
        }
    }
}