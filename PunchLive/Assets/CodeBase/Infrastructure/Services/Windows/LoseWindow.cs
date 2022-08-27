using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Services.Windows
{
    public class LoseWindow : MonoBehaviour
    {
        [SerializeField] private Button _restartLevelButton;
        
        public event Action OnRestartLevel;

        private void OnEnable()
        {
            _restartLevelButton.onClick.AddListener(() => OnRestartLevel.Invoke());
        }

        private void OnDisable()
        {
            _restartLevelButton.onClick.RemoveAllListeners();
        }
    }
}