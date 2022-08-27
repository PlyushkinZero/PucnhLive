using CodeBase.Infrastructure.Services.Windows;

namespace CodeBase.Infrastructure.Services.UI
{
    public interface IUIFactory : IService
    {
        Hud CreateHud();
        WinWindow CreateWinWindow();
        LoseWindow CreateLoseWindow();
    }
}