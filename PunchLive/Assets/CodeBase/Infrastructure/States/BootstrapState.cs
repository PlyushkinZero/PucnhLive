using CodeBase.Battle.Logic.Battle;
using CodeBase.Infrastructure.LevelManagement;
using CodeBase.Infrastructure.PersistantProgress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetManagment;
using CodeBase.Infrastructure.Services.Factories;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.UI;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.States.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;
        private readonly RectTransform _uiRoot;
        private readonly InputPanel _inputPanel;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LevelManager _levelManager;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator services, 
            RectTransform uiRoot, InputPanel inputPanel, ICoroutineRunner coroutineRunner, LevelManager levelManager)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _uiRoot = uiRoot;
            _inputPanel = inputPanel;
            _coroutineRunner = coroutineRunner;
            _levelManager = levelManager;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevelState);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevelState()
        {
            _services.Single<ISaveLoadService>().LoadProgress();
            
            int levelIndex = _services.Single<IPersistantProgressService>().PlayerProgress.LevelIndex;
            _levelManager.CurrentLevel = _levelManager.Levels[levelIndex];
            _stateMachine.Enter<LoadLevelState, string>(_levelManager.CurrentLevel.SceneName);
        }

        private void RegisterServices()
        { 
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IInputService>(new MobileInputService(_inputPanel));
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(), _uiRoot));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IEnemyFactory>(new EnemyFactory(_services.Single<IAssetProvider>(), _levelManager));
            _services.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistantProgressService>(), _levelManager));
            _services.RegisterSingle<IPlayerFactory>(new PlayerFactory(_services.Single<IAssetProvider>(), _services.Single<IPersistantProgressService>()));
            _services.RegisterSingle<IAttackReadingService>(new AttackReadingService(_services.Single<IInputService>(), _services.Single<IPlayerFactory>(), _coroutineRunner));
        }
    }
}