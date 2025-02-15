using UnityEngine;

namespace FsmScripts.States
{
    public class FallState : MoveableState
    {
        private Vector2 _direction;

        public FallState(Fsm fsm, CharacterController characterController, IProvideAbleAngle angle, MoveableStateConfig moveableStateConfig) : base(fsm, characterController, angle)
        {
            _velocity = moveableStateConfig.Velocity;
            _speed = moveableStateConfig.Speed;
            _gravity = moveableStateConfig.Gravity;
            _groundCheckSphereRadius = moveableStateConfig.GroundCheckSphereRadius;
            _interpolationCoefficient = moveableStateConfig.InterpolationCoefficient;
        }

        public override void Enter()
        {
            base.Enter();
            _velocity = 2;
        }

        public override void Update()
        {
            _direction = ReadInput();
        }

        public override void FixedUpdate()
        {
            if (_direction == Vector2.zero && IsOnGround() == true)
            {
                _fsm.SetState<IdleState>();
            }
            else if (IsOnGround())
            {
                _fsm.SetState<RunState>();
            }

            ApplyGravity();
            Move(_direction);
        }
    }

}