using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetManagment;
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
        private const string MainLevelName = "MainLevel";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;
        private readonly RectTransform _uiRoot;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator services, RectTransform uiRoot)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _uiRoot = uiRoot;

            RegisterServices();
        }

        public void Enter() 
            => _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevelState);

        public void Exit()
        {
        }

        private void EnterLoadLevelState() 
            => _stateMachine.Enter<LoadLevelState, string>(MainLevelName);

        private void RegisterServices()
        { 
            _services.RegisterSingle<IInputService>(new MobileInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(), _uiRoot));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
        }
    }
}