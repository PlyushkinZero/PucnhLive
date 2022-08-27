using System;

namespace CodeBase.Infrastructure.PersistantProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        public event Action WriteProgress;
    }
}