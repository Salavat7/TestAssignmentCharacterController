using UnityEngine;

[CreateAssetMenu(fileName = "JumpStateConfig", menuName = "Scriptable Objects/JumpStateConfig")]
public class JumpStateConfig : MoveableStateConfig
{
    [field: SerializeField] public float JumpHeight { get; private set; }
}
