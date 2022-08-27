using CodeBase.Infrastructure.Data;

namespace CodeBase.Infrastructure.PersistantProgress
{
    public interface ISavedProgressReader
    {
        public void ReadProgress(PlayerProgress progress);
    }
}