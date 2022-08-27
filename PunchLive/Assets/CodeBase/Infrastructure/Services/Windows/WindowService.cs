using System;
using CodeBase.Infrastructure.Services.UI;

namespace CodeBase.Infrastructure.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        public WinWindow WinWindow { get; private set; }
        public LoseWindow LoseWindow { get; private set; }
        public Hud Hud { get; private set; }


        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Hud:
                    Hud = _uiFactory.CreateHud();
                    break;
                case WindowId.WinWindow:
                    WinWindow = _uiFactory.CreateWinWindow();
                    break;
                case WindowId.LoseWindow:
                    LoseWindow = _uiFactory.CreateLoseWindow();
                    break;
            }
        }
    }
}