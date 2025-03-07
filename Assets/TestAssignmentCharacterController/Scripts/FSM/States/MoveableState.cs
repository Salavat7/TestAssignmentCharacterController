using UnityEngine;

namespace FsmScripts.States
{
    public abstract class MoveableState : State
    {
        protected readonly CharacterController _characterController;
        protected readonly IProvideAbleAngle _angle;
        protected float _velocity;
        protected float _speed;
        protected float _gravity;
        protected float _groundCheckSphereRadius;
        protected float _interpolationCoefficient;
        protected Vector2 _direction;

        public MoveableState(Fsm fsm, CharacterController characterController, IProvideAbleAngle angle) : base(fsm)
        {
            _characterController = characterController;
            _angle = angle;
        }

        public override void Update()
        {
            base.Update();
            _direction = ReadInput();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            ApplyGravity();
            Move(_direction);
        }

        protected Vector2 ReadInput()
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return direction;
        }

        protected void ApplyGravity()
        {
            _velocity += _gravity * Time.fixedDeltaTime;
            _characterController.Move(Vector3.down * _velocity * Time.fixedDeltaTime);
        }

        protected void Move(Vector2 direction)
        {
            Vector3 directionV3 = new Vector3(direction.x, 0, direction.y) * _speed * Time.fixedDeltaTime;
            directionV3 = Quaternion.Euler(0, _angle.CurrentRotationY, 0) * directionV3;
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


