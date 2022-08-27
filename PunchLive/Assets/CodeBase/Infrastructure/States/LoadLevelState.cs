using System;
using CodeBase.Battle.Logic.Battle;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.States.Interfaces;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly InputPanel _inputPanel;
        private readonly IAttackReadingService _attackReading;
        private readonly IWindowService _windowService;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
            IPlayerFactory playerFactory, IEnemyFactory enemyFactory, InputPanel inputPanel,
            IAttackReadingService attackReading, IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
            _inputPanel = inputPanel;
            _attackReading = attackReading;
            _windowService = windowService;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {

        }

        private void OnLoaded()
        {
            SpawnFighters();
            ShowUI();
            StartReadingAttacks();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void SpawnFighters()
        {
            _playerFactory.Spawn();
            _enemyFactory.Spawn();
        }

        private void ShowUI()
        { 
            _windowService.Open(WindowId.Hud);
            AttachHealthBars();
            _inputPanel.gameObject.SetActive(true);
        }

        private void StartReadingAttacks()
        {
            _attackReading.InitAttacksDictionary();
            _attackReading.StartReading();
        }

        private void AttachHealthBars()
        {
            _windowService.Hud.Player = _playerFactory.Damageable;
            _windowService.Hud.Enemy = _enemyFactory.Damageable;
            _windowService.Hud.StartReadingHp();
        }
    }
}