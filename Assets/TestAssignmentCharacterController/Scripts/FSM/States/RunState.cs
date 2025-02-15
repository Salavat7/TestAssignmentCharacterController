using UnityEngine;

namespace FsmScripts.States
{
    public class RunState : MoveableState
    {
        public RunState(Fsm fsm, CharacterController characterController, IProvideAbleAngle angle, MoveableStateConfig moveableStateConfig) : base(fsm, characterController, angle)
        {
            _speed = moveableStateConfig.Speed;
            _gravity = moveableStateConfig.Gravity;
            _groundCheckSphereRadius = moveableStateConfig.GroundCheckSphereRadius;
            _interpolationCoefficient = moveableStateConfig.InterpolationCoefficient;
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fsm.SetState<JumpState>();
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (IsOnGround() && _direction == Vector2.zero)
            {
                _fsm.SetState<IdleState>();
            }

            if (IsOnGround())
            {
                _velocity = 0;
            }
            else
            {
                _fsm.SetState<FallState>();
            }
        }
    }
}