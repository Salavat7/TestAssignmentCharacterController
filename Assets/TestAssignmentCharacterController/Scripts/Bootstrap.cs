using UnityEngine;
using FsmScripts;
using FsmScripts.States;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] CharacterController _characterController;

    Fsm _fsm;

    private void Awake()
    {
        _fsm = new Fsm();

        _fsm.AddState(new IdleState(_fsm));
        _fsm.AddState(new RunState(_fsm, _characterController));
        _fsm.AddState(new JumpState(_fsm, _characterController));

        _fsm.SetState<IdleState>();
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
