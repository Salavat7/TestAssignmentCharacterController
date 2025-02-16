using System;
using UnityEngine;

public class DesktopInput : IInput
{
    public event Action<Vector2> Moved;
    public event Action<Vector2> Looked;
    public event Action<bool> Jumped;

    public void ReadInput()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        bool jump = Input.GetKeyDown(KeyCode.Space);

        Moved?.Invoke(move);
        Looked?.Invoke(look);
        Jumped?.Invoke(jump);
    }
}
