using UnityEngine;

[CreateAssetMenu(fileName = "FallStateConfig", menuName = "Scriptable Objects/FallStateConfig")]
public class FallStateConfig : MoveableStateConfig
{
    [field: SerializeField] public float VelocityToStartFall { get; private set; }
}
