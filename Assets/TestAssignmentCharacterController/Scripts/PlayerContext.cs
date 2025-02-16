using FsmScripts;
using FsmScripts.States;
using UnityEngine;

public class PlayerContext : MonoBehaviour
{
    private Fsm _fsm;
    private PlayerAnimations _animations;

    public void Init(Fsm fsm, PlayerAnimations playerAnimations)
    {
        _fsm = fsm;
        _animations = playerAnimations;

        _fsm.GetState<RunState>().Entered += OnRunStateEntered;
        _fsm.GetState<RunState>().Exited += OnRunStateExited;
        _fsm.GetState<JumpState>().Entered += OnJumpStateEntered;
        _fsm.GetState<JumpState>().Exited += OnJumpStateExited;
        _fsm.GetState<FallState>().Entered += OnFallStateEntered;
        _fsm.GetState<FallState>().Exited += OnFallStateExited;
    }

    private void Update()
    {
        _fsm?.Update();
    }

    private void FixedUpdate()
    {
        _fsm?.FixedUpdate();
    }

    private void OnRunStateEntered() => _animations.Run = true;
    private void OnRunStateExited() => _animations.Run = false;
    private void OnJumpStateEntered() => _animations.Jump = true;
    private void OnJumpStateExited() => _animations.Jump = false;
    private void OnFallStateEntered() => _animations.Fall = true;
    private void OnFallStateExited() => _animations.Fall = false;

    private void OnDestroy()
    {
        _fsm.GetState<RunState>().Entered -= OnRunStateEntered;
        _fsm.GetState<RunState>().Exited -= OnRunStateExited;
        _fsm.GetState<JumpState>().Entered -= OnJumpStateEntered;
        _fsm.GetState<JumpState>().Exited -= OnJumpStateExited;
        _fsm.GetState<FallState>().Entered -= OnFallStateEntered;
        _fsm.GetState<FallState>().Exited -= OnFallStateExited;
    }

}
