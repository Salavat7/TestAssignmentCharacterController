using UnityEngine;

namespace FsmScripts.States
{
    public class JumpState : MoveableState
    {
        private float _jumpHeight;
        private Vector2 _direction;

        public JumpState(Fsm fsm, CharacterController characterController, IProvideAbleAngle angle, JumpStateConfig jumpStateConfig) : base(fsm, characterController, angle)
        {
            _velocity = jumpStateConfig.Velocity;
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

        public override void Update()
        {
            _direction = ReadInput();
            // Debug.Log("JumpState");
        }

        public override void FixedUpdate()
        {
            if (IsOnGround() && _velocity > 0 && _direction == Vector2.zero)
            {
                _fsm.SetState<IdleState>();
            }
            else if (IsOnGround() && _velocity > 0)
            {
                _fsm.SetState<RunState>();
            }

            ApplyGravity();
            Move(_direction);
        }

        private void Jump()
        {
            _velocity = -Mathf.Sqrt(_jumpHeight * _gravity);
        }
    }
}