using System;
using CodeBase.Infrastructure.Services.UI.Elements;
using CodeBase.Infrastructure.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private readonly InputPanel _inputPanel;
        private bool _blockingState = false;
        public event Action<ButtonType> OnButtonClick;
        public event Action<bool> OnChangeBlockingState;


        public MobileInputService(InputPanel inputPanel)
        {
            _inputPanel = inputPanel;
        }


        public void EnableInput()
        {
            foreach (ButtonWithType button in _inputPanel.ButtonList)
            {
                button.Button.onClick.AddListener(() => OnButtonClick?.Invoke(button.Type));
                if (button.Type == ButtonType.Block)
                {
                    button.Button.OnPointerDownEvent += ChangeBlockingState;
                    button.Button.OnPointerUpEvent += ChangeBlockingState;
                }
            }
        }

        public void DisableInput()
        {
            foreach (ButtonWithType button in _inputPanel.ButtonList)
            {
                button.Button.onClick.RemoveAllListeners();
                if (button.Type == ButtonType.Block)
                {
                    button.Button.OnPointerDownEvent -= ChangeBlockingState;
                    button.Button.OnPointerUpEvent -= ChangeBlockingState;
                }
            }
        }

        private void ChangeBlockingState()
        {
            _blockingState = !_blockingState;
            OnChangeBlockingState?.Invoke(_blockingState);
        }
    }
}