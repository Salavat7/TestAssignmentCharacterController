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

        _fsm.GetState<RunState>().Entered += () => _animations.Run = true;
        _fsm.GetState<RunState>().Exited += () => _animations.Run = false;
        _fsm.GetState<JumpState>().Entered += () => _animations.Jump = true;
        _fsm.GetState<JumpState>().Exited += () => _animations.Jump = false;        
    }
}
