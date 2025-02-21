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
    private DesktopInput _desktopInput;
    private RelativeInputDecorator _relativeInputDecorator;

    private void Awake()
    {
        _desktopInput = new DesktopInput();
        _relativeInputDecorator = new RelativeInputDecorator(_desktopInput, _playerCamera);
        _fsm = FsmInit(_characterController, _relativeInputDecorator);
        _playerContext.Init(_fsm, _playerAnimations);
    }

    private Fsm FsmInit(CharacterController characterController, IInput input)
    {
        Fsm fsm = new Fsm();

        MoveableStateConfig moveableStateConfig = Resources.Load<MoveableStateConfig>("Configs/MoveableStateConfig");
        JumpStateConfig JumpStateConfig = Resources.Load<JumpStateConfig>("Configs/JumpStateConfig");
        FallStateConfig fallStateConfig = Resources.Load<FallStateConfig>("Configs/FallStateConfig");

        fsm.AddState(new IdleState(fsm, input));
        fsm.AddState(new RunState(fsm, characterController, input, moveableStateConfig));
        fsm.AddState(new JumpState(fsm, characterController, input, JumpStateConfig));
        fsm.AddState(new FallState(fsm, characterController, input, fallStateConfig));

        fsm.SetState<IdleState>();

        return fsm;
    }

    private void Update()
    {
        _desktopInput.ReadInput();
    }

    private void OnDestroy()
    {
        _relativeInputDecorator.Dispose();
    }
}
