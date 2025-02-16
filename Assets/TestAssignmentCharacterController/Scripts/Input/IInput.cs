using System;
using UnityEngine;

public interface IInput
{
    public event Action<Vector2> Moved;
    public event Action<Vector2> Looked;
    public event Action<bool> Jumped;
}
