using System;
using UnityEngine;

public class InputForCharacterController : IInput, IDisposable
{
    public event Action<Vector2> Moved;
    public event Action<Vector2> Looked;
    public event Action<bool> Jumped;

    private IInput _input;
    private IProvideAbleAngle _angle;

    public InputForCharacterController(IInput input, IProvideAbleAngle provideAbleAngle)
    {
        _input = input;
        _angle = provideAbleAngle;

        _input.Moved += OnMoved;
        _input.Looked += OnLooked;
        _input.Jumped += OnJumped;
    }

    private void OnMoved(Vector2 move)
    {
        Vector3 move3 = new Vector3(move.x, 0, move.y) * Time.fixedDeltaTime;
        move3 = Quaternion.Euler(0, _angle.CurrentRotationY, 0) * move3;
        Moved?.Invoke(new Vector2(move3.x, move3.z));
    }

    private void OnLooked(Vector2 look)
    {
        Vector2 vector = new Vector2(look.y, look.x);
        Looked?.Invoke(vector);
    }

    private void OnJumped(bool jump)
    {
        Jumped?.Invoke(jump);
    }

    public void Dispose()
    {
        _input.Moved -= OnMoved;
        _input.Looked -= OnLooked;
        _input.Jumped -= OnJumped;
    }
}
