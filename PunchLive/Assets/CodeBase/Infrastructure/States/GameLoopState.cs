using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.States.Interfaces;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;

        public GameLoopState(GameStateMachine gameStateMachine, IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
        }

        public void Enter()
        {
            _inputService.EnableInput();
        }

        public void Exit()
        {
            
        }
    }
}