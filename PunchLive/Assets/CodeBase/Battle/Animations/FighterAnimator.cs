using UnityEngine;

namespace CodeBase.Battle.Animations
{
    public class FighterAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake() 
            => _animator = GetComponent<Animator>();

        public void PlayAttack(string attackAnimatorKey) 
            => _animator.SetTrigger(AnimationKeys.Keys[attackAnimatorKey]);

        public void PlayBlocking(bool state)
        {
            _animator.SetBool(AnimationKeys.BasicBlockKey, state);
        }
    }
}