using UnityEngine;

namespace FsmScripts.States
{
    public class IdleState : State
    {
        public IdleState(Fsm fsm) : base(fsm) { }

        public override void Update()
        {
            if(ReadInput() != Vector2.zero)
            {
                _fsm.SetState<RunState>();
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                _fsm.SetState<JumpState>();
            }

            Debug.Log("IdleState");
        }

        private Vector2 ReadInput()
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return direction;
        }
    }
}