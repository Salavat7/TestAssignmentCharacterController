using UnityEngine;
using FsmScripts;
using FsmScripts.States;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;
    [SerializeField] PlayerAnimations _playerAnimations;
    [SerializeField] PlayerContext _playerContext;

    private Fsm _fsm;

    private void Awake()
    {
        _fsm = new Fsm();

        _fsm.AddState(new IdleState(_fsm));
        _fsm.AddState(new RunState(_fsm, _characterController));
        _fsm.AddState(new JumpState(_fsm, _characterController));

        _fsm.SetState<IdleState>();


        _playerContext.Init(_fsm, _playerAnimations);
    }

    private void Update()
    {
        _fsm.Update();
    }

    private void FixedUpdate()
    {
        _fsm.FixedUpdate();
    }
}
