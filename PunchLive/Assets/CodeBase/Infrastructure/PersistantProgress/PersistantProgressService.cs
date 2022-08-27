using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
        public FighterData PlayerStats { get; set; }
    }
}