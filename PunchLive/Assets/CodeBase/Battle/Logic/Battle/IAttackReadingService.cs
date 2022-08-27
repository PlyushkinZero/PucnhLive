using CodeBase.Infrastructure.Services;

namespace CodeBase.Battle.Logic.Battle
{
    public interface IAttackReadingService : IService
    {
        public void InitAttacksDictionary();
        public void StartReading();
        public void StopReading();
        
    }
}