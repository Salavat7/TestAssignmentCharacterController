using UnityEngine;

namespace FsmScripts.States
{
    public class RunState : MoveableState
    {
        private Vector2 _direction;

        public RunState(Fsm fsm, CharacterController characterController, PlayerCamera playerCamera, MoveableStateConfig moveableStateConfig) : base(fsm, characterController, playerCamera)
        {
            _velocity = moveableStateConfig.Velocity;
            _speed = moveableStateConfig.Speed;
            _gravity = moveableStateConfig.Gravity;
            _groundCheckSphereRadius = moveableStateConfig.GroundCheckSphereRadius;
            _interpolationCoefficient = moveableStateConfig.InterpolationCoefficient;
        }

        public override void Update()
        {
            _direction = ReadInput();

            if (_direction == Vector2.zero)
            {
                _fsm.SetState<IdleState>();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fsm.SetState<JumpState>();
            }

            // Debug.Log("RunState");
        }

        public override void FixedUpdate()
        {
            if (IsOnGround())
            {
                _velocity = 0;
            }

            ApplyGravity();
            Move(_direction);
        }
    }
}