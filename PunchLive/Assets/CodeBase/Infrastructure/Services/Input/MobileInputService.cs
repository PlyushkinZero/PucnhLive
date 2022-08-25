using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeBase.Infrastructure.Services.Input
{
    public class MobileInputService : IInputService
    {
        private DefaultActions.GameLoopActions _inputActions;
        private Camera _mainCamera;
        
        public event Action<Vector2> OnClickPosition;
        public event Action<Ray> OnClickRay;

        public MobileInputService()
        {
            _inputActions = new DefaultActions().GameLoop;
            _inputActions.Click.performed += _ => OnCLickInvoke();
            EnableInput();
        }

        public void EnableInput()
            => _inputActions.Enable();

        public void DisableInput()
            => _inputActions.Disable();

        private void OnCLickInvoke()
        {
            _mainCamera ??= Camera.main;
            Vector2 screenPosition = _inputActions.Position.ReadValue<Vector2>();
            
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);
            OnClickPosition?.Invoke(worldPosition);
            
            Ray clickRay = _mainCamera.ScreenPointToRay(screenPosition);
            OnClickRay?.Invoke(clickRay);
        }
    }
}