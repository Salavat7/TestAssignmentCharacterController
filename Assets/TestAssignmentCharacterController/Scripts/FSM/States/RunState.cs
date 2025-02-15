using UnityEngine;

namespace FsmScripts.States
{
    public class RunState : State
    {
        private readonly CharacterController _characterController;
        private readonly PlayerCamera _playerCamera;

        private float _speed = 5;
        private float _gravity = 9.81f;
        private float _velocity = 0;
        private float _groundCheckSphereRadius = 0.1f;
        private float _interpolationCoefficient = 0.3f;
        private Vector2 _direction;

        public RunState(Fsm fsm, CharacterController characterController, PlayerCamera playerCamera) : base(fsm)
        {
            _characterController = characterController;
            _playerCamera = playerCamera;
        }

        public override void Update()
        {
            _direction = ReadInput();

            if (_direction == Vector2.zero)
            {
                _fsm.SetState<IdleState>();
            }

            if(Input.GetKeyDown(KeyCode.Space))
            {
                _fsm.SetState<JumpState>();
            }

            Debug.Log("RunState");
        }

        public override void FixedUpdate()
        {
            if(IsOnGround())
            {
                _velocity = 0;
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
            directionV3 = Quaternion.Euler(0, _playerCamera.CurrentRotationY, 0) * directionV3;
            // directionV3 = Quaternion.Euler(0, 90, 0) * directionV3;
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
    }
}