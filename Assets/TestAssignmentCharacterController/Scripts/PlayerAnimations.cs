using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public bool Run { set => _animator.SetBool("Run", value); }
    public bool Jump { set => _animator.SetBool("Jump", value); }
}
