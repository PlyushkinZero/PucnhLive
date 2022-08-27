using System.Collections.Generic;
using CodeBase.Infrastructure.Services.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Windows
{
    public class InputPanel : MonoBehaviour
    {
        [field: SerializeField] public List<ButtonWithType> ButtonList { get; private set; }
    }
}