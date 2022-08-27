using System;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.LevelManagement
{
    [Serializable]
    public class Level
    {
        [field: SerializeField] public int IndexInView { get; private set; }
        [field: SerializeField] public FighterData Enemy { get; private set; }
        [field: SerializeField] public string SceneName { get; private set; }
    }
}