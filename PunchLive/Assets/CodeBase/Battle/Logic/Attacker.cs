using System;
using System.Collections.Generic;
using CodeBase.Battle.Animations;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Battle.Logic
{
    [RequireComponent(typeof(FighterAnimator))]
    public class Attacker : MonoBehaviour
    {
        private int _damageMultiplier;
        private FighterAnimator _animator;
        private TeamType _team;
        private IPlayerFactory _playerFactory;
        private IEnemyFactory _enemyFactory;

        public event Action OnAttackInited; 
        public List<Attack> EnabledAttacks { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<FighterAnimator>();
            _playerFactory = ServiceLocator.Container.Single<IPlayerFactory>();
            _enemyFactory = ServiceLocator.Container.Single<IEnemyFactory>();
        }

        public void Attacking(Attack attack)
        {
            OnAttackInited?.Invoke();
            _animator.PlayAttack(attack.AnimatorKey);
        }

        public void TakeDamageToOpponent(Attack attack) // AnimationEvent
        {
            switch (_team.OppositeTeam())
            {
                case TeamType.Player:
                    _playerFactory.Damageable.TakeDamage(attack.Damage * _damageMultiplier);
                    break;
                case TeamType.Enemy:
                    _enemyFactory.Damageable.TakeDamage(attack.Damage * _damageMultiplier);
                    break;
            }
        }

        public void AttachData(FighterData data, TeamType team)
        {
            _damageMultiplier = data.DamageMultiplier;
            EnabledAttacks = data.Attacks;
            _team = team;
        }
    }
}