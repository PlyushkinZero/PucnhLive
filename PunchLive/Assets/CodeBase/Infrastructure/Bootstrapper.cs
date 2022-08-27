using CodeBase.Infrastructure.LevelManagement;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private RectTransform _uiRoot;
        [SerializeField] private InputPanel _inputPanel;
        [SerializeField] private LevelManager _levelManager;
        private void Awake()
        {
            Game game = new Game(this, _uiRoot, _inputPanel, _levelManager);
            game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}