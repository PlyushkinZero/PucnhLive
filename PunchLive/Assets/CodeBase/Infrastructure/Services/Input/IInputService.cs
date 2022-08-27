using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        event Action<ButtonType> OnButtonClick; 
        event Action<bool> OnChangeBlockingState;
        
        void EnableInput();
        void DisableInput();
    }
}