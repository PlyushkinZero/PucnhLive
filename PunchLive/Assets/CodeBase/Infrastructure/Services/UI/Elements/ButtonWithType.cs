using System;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.UI.Elements
{
    [Serializable]
    public class ButtonWithType
    {
        [field: SerializeField] public CustomButton Button { get; private set; }
        [field: SerializeField] public ButtonType Type { get; private set; }
    }
}