using CodeBase.Infrastructure.Services.Windows;

namespace CodeBase.Infrastructure.Services
{
    public interface IWindowService : IService
    {
        PauseWindow PauseWindow { get; }
        void Open(WindowId windowId);
    }
}