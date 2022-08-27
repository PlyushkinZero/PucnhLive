using CodeBase.Battle.Logic;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.Services.Factories.Interfaces
{
    public interface IPlayerFactory : IFighterFactory
    {
        public FighterData Data { get; }
        public Attacker Attacker { get; }
        public Damageable Damageable { get; }
    }
}