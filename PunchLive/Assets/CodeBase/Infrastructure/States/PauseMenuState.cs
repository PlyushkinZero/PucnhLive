using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.UI;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.States.Interfaces;

namespace CodeBase.Infrastructure.States
{
    public class PauseMenuState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly IWindowService _windowService;

        public PauseMenuState(GameStateMachine stateMachine, IInputService inputService, IWindowService windowService)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _windowService = windowService;
        }

        public void Enter()
        {
            _inputService.EnableInput();
            _windowService.Open(WindowId.Pause);
            _windowService.PauseWindow.OnClick += StartGame;
        }

        public void Exit()
        {

        }

        private void StartGame()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}