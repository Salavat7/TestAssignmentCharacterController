using UnityEngine;

namespace FsmScripts.States
{
    public class JumpState : State
    {
        private readonly CharacterController _characterController;

        private float _jumpHeight = 3;
        private float _speed = 10;
        private float _gravity = 9.81f;
        private float _velocity = 0;
        private float _groundCheckSphereRadius = 0.1f;
        private float _interpolationCoefficient = 0.3f;
        private Vector2 _direction;

        public JumpState(Fsm fsm, CharacterController characterController) : base(fsm)
        {
            _characterController = characterController;
        }

        public override void Enter()
        {
            Jump();
        }

        public override void Update()
        {
            _direction = ReadInput();

            Debug.Log("JumpState");
        }

        public override void FixedUpdate()
        {
            if (IsOnGround() && _velocity > 0 && _direction == Vector2.zero)
            {
                _fsm.SetState<IdleState>();
            }
            else if(IsOnGround() && _velocity > 0)
            {
                _fsm.SetState<RunState>();
            }

            ApplyGravity();
            Move(_direction);
        }

        private Vector2 ReadInput()
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return direction;
        }

        private void ApplyGravity()
        {
            _velocity += _gravity * Time.fixedDeltaTime;
            _characterController.Move(Vector3.down * _velocity * Time.fixedDeltaTime);
        }

        private void Move(Vector2 direction)
        {
            Vector3 directionV3 = new Vector3(direction.x, 0, direction.y) * _speed * Time.fixedDeltaTime;
            _characterController.Move(directionV3);

            _characterController.transform.forward = Vector3.Lerp(_characterController.transform.forward, directionV3.normalized, _interpolationCoefficient);
        }

        private bool IsOnGround()
        {
            Vector3 groundCheckPos = _characterController.bounds.center;
            groundCheckPos.y = groundCheckPos.y - _characterController.bounds.size.y / 2;
            bool isOnGound = Physics.CheckSphere(groundCheckPos, _groundCheckSphereRadius);

            return isOnGound;
        }

        private void Jump()
        {
            _velocity = -Mathf.Sqrt(_jumpHeight * _gravity);
        }
    }
}