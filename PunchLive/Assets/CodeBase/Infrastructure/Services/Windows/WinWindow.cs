using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Services.Windows
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private Button _nextLevelButton;
        
        public event Action OnGoToNextLevel;

        private void OnEnable()
        {
            _nextLevelButton.onClick.AddListener(GoToNextLevel);
        }

        private void GoToNextLevel()
        {
            OnGoToNextLevel.Invoke();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _nextLevelButton.onClick.RemoveAllListeners();
        }
    }
}