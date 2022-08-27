using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Create FighterData", fileName = "FighterData", order = 0)]
    public class FighterData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public int Health = 100;
        public int DamageMultiplier = 1;
        public List<Attack> Attacks;
        
        public float ChanceToStandUp = 0.10f;
        public float ChanceToBlock = 0.30f;
        public float MinimalPauseBetweenAttacks = 0.5f;
        public float MaximalPauseBetweenAttacks = 3.5f;
    }
}