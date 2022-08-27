using CodeBase.Battle.Logic;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories.Interfaces
{
    public interface IFighterFactory : IService
    {
        public FighterData Data { get; }
        public Attacker Attacker { get; }
        public GameObject Spawn();
    }
}