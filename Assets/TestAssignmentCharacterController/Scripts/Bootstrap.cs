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

    private Fsm FsmInit(CharacterController characterController, PlayerCamera playerCamera)
    {
        Fsm fsm = new Fsm();

        MoveableStateConfig moveableStateConfig = Resources.Load<MoveableStateConfig>("Configs/MoveableStateConfig");
        JumpStateConfig JumpStateConfig = Resources.Load<JumpStateConfig>("Configs/JumpStateConfig");

        fsm.AddState(new IdleState(fsm));
        fsm.AddState(new RunState(fsm, characterController, playerCamera, moveableStateConfig));
        fsm.AddState(new JumpState(fsm, characterController, playerCamera, JumpStateConfig));
        fsm.AddState(new FallState(fsm, characterController, playerCamera, moveableStateConfig));

        fsm.SetState<IdleState>();

        return fsm;
    }
}
