using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Battle.Animations
{
    public static class AnimationKeys
    {
        public static readonly int BasicLeftKey = Animator.StringToHash("BasicLeft");
        public static readonly int BasicRightKey = Animator.StringToHash("BasicRight");
        public static readonly int BasicBlockKey = Animator.StringToHash("BasicBlock");

        public static readonly Dictionary<string, int> Keys = new()
        {
            {"BasicLeft", BasicLeftKey},
            {"BasicRight", BasicRightKey},
            {"BasicBlock", BasicBlockKey}
        };
    }
}