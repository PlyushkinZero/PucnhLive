using CodeBase.Infrastructure.Services.UI;
using CodeBase.Infrastructure.Services.Windows;

namespace CodeBase.Infrastructure.Services
{
    public interface IWindowService : IService
    {
        Hud Hud { get; }
        WinWindow WinWindow { get; }
        LoseWindow LoseWindow { get; }
        void Open(WindowId windowId);
    }
}