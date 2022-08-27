using CodeBase.Infrastructure.LevelManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    internal class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, RectTransform uiRoot, InputPanel inputPanel, LevelManager levelManager) 
            => StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), ServiceLocator.Container, uiRoot, inputPanel, coroutineRunner, levelManager);
    }
}