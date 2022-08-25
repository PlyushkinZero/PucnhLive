using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        event Action<Vector2> OnClickPosition;
        event Action<Ray> OnClickRay;
        
        void EnableInput();
        void DisableInput();
    }
}