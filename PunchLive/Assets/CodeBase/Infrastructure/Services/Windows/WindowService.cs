using CodeBase.Infrastructure.Services.UI;

namespace CodeBase.Infrastructure.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;
        
        public PauseWindow PauseWindow { get; private set; }

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
                case WindowId.Pause:
                    PauseWindow = _uiFactory.CreatePauseWindow();
                    break;
            }
        }
    }
}