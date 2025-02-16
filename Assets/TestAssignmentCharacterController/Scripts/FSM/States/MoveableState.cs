using UnityEngine;

namespace FsmScripts.States
{
    public abstract class MoveableState : State
    {
        protected readonly CharacterController _characterController;
        protected readonly IInput _iInput;
        protected float _velocity;
        protected float _speed;
        protected float _gravity;
        protected float _groundCheckSphereRadius;
        protected float _interpolationCoefficient;
        protected Vector2 _direction;

        public MoveableState(Fsm fsm, CharacterController characterController, IInput iInput) : base(fsm)
        {
            _characterController = characterController;
            _iInput = iInput;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            ApplyGravity();
            Move(_direction);
        }

        public override void Enter()
        {
            base.Enter();
            _iInput.Moved += OnMoved;
        }

        public override void Exit()
        {
            base.Exit();
            _iInput.Moved -= OnMoved;
        }

        protected void OnMoved(Vector2 vector)
        {
            _direction = vector;
        }

        protected void ApplyGravity()
        {
            _velocity += _gravity * Time.fixedDeltaTime;
            _characterController.Move(Vector3.down * _velocity * Time.fixedDeltaTime);
        }

        protected void Move(Vector2 direction)
        {
            Vector3 directionV3 = new Vector3(direction.x, 0, direction.y) * _speed;
            _characterController.Move(directionV3);

            _characterController.transform.forward = Vector3.Lerp(_characterController.transform.forward, directionV3.normalized, _interpolationCoefficient);
        }

        protected bool IsOnGround()
        {
            Vector3 groundCheckPos = _characterController.bounds.center;
            groundCheckPos.y = groundCheckPos.y - _characterController.bounds.size.y / 2;
            bool isOnGound = Physics.CheckSphere(groundCheckPos, _groundCheckSphereRadius);

            return isOnGound;
        }
    }
}


