using System;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public int LevelIndex { get; set; } = 0;
        public FighterData PlayerStats { get; set; }
    }
}