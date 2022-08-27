using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.LevelManagement
{
    public class LevelManager : MonoBehaviour
    {
        [field: SerializeField] public FighterData InitialPlayerData;
        [field: SerializeField] public bool UseSavedProgress { get; set; }
        [field: SerializeField] public List<Level> Levels { get; private set; }
        public Level CurrentLevel { get; set; }
    }
}