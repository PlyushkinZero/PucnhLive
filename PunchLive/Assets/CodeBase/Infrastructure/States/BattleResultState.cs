using System;
using CodeBase.Battle.Logic;
using CodeBase.Infrastructure.LevelManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.States.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BattleResultState : IPayloadedState<TeamType>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IWindowService _windowService;
        private readonly LevelManager _levelManager;

        public BattleResultState(GameStateMachine gameStateMachine, IWindowService windowService, LevelManager levelManager)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _levelManager = levelManager;
        }
        
        public void Enter(TeamType result)
        {
            switch (result)
            {
                case TeamType.Player:
                    _windowService.Open(WindowId.WinWindow);
                    _windowService.WinWindow.OnGoToNextLevel += StartNextLevel;
                    break;
                case TeamType.Enemy:
                    _windowService.Open(WindowId.LoseWindow);
                    _windowService.LoseWindow.OnRestartLevel += RestartLevel;
                    break;
            }
        }

        private void StartNextLevel()
        {
            _windowService.WinWindow.OnGoToNextLevel -= StartNextLevel;
            int currentLevelIndex = _levelManager.CurrentLevel.IndexInView;
            _gameStateMachine.Enter<LoadLevelState, string>(_levelManager.Levels[currentLevelIndex].SceneName);
        }

        private void RestartLevel()
        {
            _windowService.LoseWindow.OnRestartLevel -= RestartLevel;
            _gameStateMachine.Enter<LoadLevelState, string>(_levelManager.CurrentLevel.SceneName);
        }

        public void Exit()
        {
            
        }
    }
}