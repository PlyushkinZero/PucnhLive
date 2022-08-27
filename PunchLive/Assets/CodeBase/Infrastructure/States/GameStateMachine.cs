using System;
using System.Collections.Generic;
using CodeBase.Battle.Logic.Battle;
using CodeBase.Infrastructure.LevelManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.States.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, ServiceLocator serviceLocator,
            RectTransform uiRoot, InputPanel inputPanel, ICoroutineRunner coroutineRunner, LevelManager levelManager)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator, uiRoot, inputPanel, coroutineRunner, levelManager),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, serviceLocator.Single<IPlayerFactory>(), serviceLocator.Single<IEnemyFactory>(), inputPanel, serviceLocator.Single<IAttackReadingService>(), serviceLocator.Single<IWindowService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, serviceLocator.Single<IInputService>()),
                [typeof(BattleResultState)] = new BattleResultState(this, serviceLocator.Single<IWindowService>(), levelManager)
            };
        }
            
        public void Enter<TState>() where TState : class, IState 
            => ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> 
            => ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
                
            TState state = GetState<TState>();
            _activeState = state;
                
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState 
            => _states[typeof(TState)] as TState;
    }
}