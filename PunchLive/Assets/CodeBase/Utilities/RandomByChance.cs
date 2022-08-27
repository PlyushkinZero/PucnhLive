using UnityEngine;

namespace CodeBase.Utilities
{
    public static class RandomByChance
    {
        public static bool Check(float chance)
        {
            return Random.Range(0f, 1f) < chance;
        }
    }
}