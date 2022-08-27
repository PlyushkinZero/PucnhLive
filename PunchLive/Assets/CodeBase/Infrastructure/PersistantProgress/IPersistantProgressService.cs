using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.PersistantProgress
{
    public interface IPersistantProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}