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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _fsm.SetState<JumpState>();
            }

            // Debug.Log("RunState");
        }

        public override void FixedUpdate()
        {
            if (_direction == Vector2.zero && IsOnGround() == true)
            {
                _fsm.SetState<IdleState>();
            }

            if (IsOnGround())
            {
                _velocity = 0;
            }

            ApplyGravity();
            Move(_direction);
        }
    }
}