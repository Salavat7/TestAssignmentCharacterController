using UnityEngine;

namespace FsmScripts.States
{
    public class JumpState : MoveableState
    {
        private float _jumpHeight;

        public JumpState(Fsm fsm, CharacterController characterController, IProvideAbleAngle angle, JumpStateConfig jumpStateConfig) : base(fsm, characterController, angle)
        {
            _speed = jumpStateConfig.Speed;
            _gravity = jumpStateConfig.Gravity;
            _groundCheckSphereRadius = jumpStateConfig.GroundCheckSphereRadius;
            _interpolationCoefficient = jumpStateConfig.InterpolationCoefficient;
            _jumpHeight = jumpStateConfig.JumpHeight;
        }

        public override void Enter()
        {
            base.Enter();
            Jump();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (IsOnGround() && _direction == Vector2.zero && _velocity > 0)
            {
                _fsm.SetState<IdleState>();
            }
            else if (IsOnGround() && _velocity > 0)
            {
                _fsm.SetState<RunState>();
            }
        }

        private void Jump()
        {
            _velocity = -Mathf.Sqrt(_jumpHeight * _gravity);
        }
    }
}