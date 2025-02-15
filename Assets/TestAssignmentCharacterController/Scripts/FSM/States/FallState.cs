using UnityEngine;

namespace FsmScripts.States
{
    public class FallState : MoveableState
    {
        private float _velocityToStartFall;

        public FallState(Fsm fsm, CharacterController characterController, IProvideAbleAngle angle, FallStateConfig fallStateConfig) : base(fsm, characterController, angle)
        {
            _speed = fallStateConfig.Speed;
            _gravity = fallStateConfig.Gravity;
            _groundCheckSphereRadius = fallStateConfig.GroundCheckSphereRadius;
            _interpolationCoefficient = fallStateConfig.InterpolationCoefficient;
            _velocityToStartFall = fallStateConfig.VelocityToStartFall;
        }

        public override void Enter()
        {
            base.Enter();
            _velocity = _velocityToStartFall;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (IsOnGround() && _direction == Vector2.zero)
            {
                _fsm.SetState<IdleState>();
            }
            else if (IsOnGround())
            {
                _fsm.SetState<RunState>();
            }
        }
    }

}