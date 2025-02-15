using UnityEngine;

[CreateAssetMenu(fileName = "MoveableStateConfig", menuName = "Scriptable Objects/MoveableStateConfig")]
public class MoveableStateConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Gravity { get; private set; }
    [field: SerializeField] public float GroundCheckSphereRadius { get; private set; }
    [field: SerializeField] public float InterpolationCoefficient { get; private set; }
}
