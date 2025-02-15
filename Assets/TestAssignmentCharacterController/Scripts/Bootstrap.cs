using UnityEngine;
using FsmScripts;
using FsmScripts.States;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerAnimations _playerAnimations;
    [SerializeField] private PlayerContext _playerContext;
    [SerializeField] private PlayerCamera _playerCamera;

    private Fsm _fsm;

    private void Awake()
    {
        _fsm = FsmInit(_characterController, _playerCamera);
        _playerContext.Init(_fsm, _playerAnimations);
    }

    private Fsm FsmInit(CharacterController characterController, IProvideAbleAngle angle)
    {
        Fsm fsm = new Fsm();

        MoveableStateConfig moveableStateConfig = Resources.Load<MoveableStateConfig>("Configs/MoveableStateConfig");
        JumpStateConfig JumpStateConfig = Resources.Load<JumpStateConfig>("Configs/JumpStateConfig");
        FallStateConfig fallStateConfig = Resources.Load<FallStateConfig>("Configs/FallStateConfig");

        fsm.AddState(new IdleState(fsm));
        fsm.AddState(new RunState(fsm, characterController, angle, moveableStateConfig));
        fsm.AddState(new JumpState(fsm, characterController, angle, JumpStateConfig));
        fsm.AddState(new FallState(fsm, characterController, angle, fallStateConfig));

        fsm.SetState<IdleState>();

        return fsm;
    }
}
