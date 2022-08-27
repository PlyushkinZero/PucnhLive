using System;

namespace CodeBase.Battle.Logic
{
    public static class TeamTypeExtension
    {
        public static TeamType OppositeTeam(this TeamType team)
        {
            switch (team)
            {
                case TeamType.Player:
                    return TeamType.Enemy;
                case TeamType.Enemy:
                    return TeamType.Player;
                default:
                    throw new ArgumentOutOfRangeException(nameof(team), team, null);
            }
        }
    }
}