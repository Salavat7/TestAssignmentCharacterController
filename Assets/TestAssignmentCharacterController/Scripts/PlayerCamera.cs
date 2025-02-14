using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offset;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _smoothTime;
    [SerializeField] private Vector2 _minMaxX;

    private Vector3 _smoothVelocity = Vector3.zero;
    private Vector2 _mouseInput;
    private Vector3 _currentRotation;
    private Vector2 _rotation;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _mouseInput = ReadInput();
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    private Vector2 ReadInput()
    {
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        return mouseInput;
    }

    private void Rotation()
    {
        _mouseInput *= _sensitivity;
        _rotation += _mouseInput;
        _rotation.y = Mathf.Clamp(_rotation.y, _minMaxX.x, _minMaxX.y);
        Vector3 nextRotation = new Vector3(_rotation.y, _rotation.x);
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);

        transform.eulerAngles = _currentRotation;
        transform.position = _player.position - transform.forward * _offset;
    }

}
