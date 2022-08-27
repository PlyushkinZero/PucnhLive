using CodeBase.Battle.Logic;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Create Attack", fileName = "Attack", order = 0)]
    public class Attack : ScriptableObject
    {
        public TeamType Team;
        public int Damage;
        public float Speed;
        public KeyBinding Binding;
        public string AnimatorKey;
    }
}