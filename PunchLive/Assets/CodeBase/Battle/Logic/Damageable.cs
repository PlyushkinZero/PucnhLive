using System;
using CodeBase.Battle.AI;
using CodeBase.Battle.Animations;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.Windows;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Battle.Logic
{
    [RequireComponent(typeof(FighterAnimator))]
    public class Damageable : MonoBehaviour
    {
        private const float BlockingDamageReduction = 1f;
        
        private IInputService _inputService;
        private FighterAnimator _animator;
        private TeamType _team;
        private BlockingLogic _blockingLogic;
        private IWindowService _windowService;

        public event Action<TeamType> OnDie;
        public event Action<TeamType> OnChangeHealth;
        
        public bool IsBlocking { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }


        private void Awake()
        {
            _animator = GetComponent<FighterAnimator>();
            _inputService = ServiceLocator.Container.Single<IInputService>();
            _windowService = ServiceLocator.Container.Single<IWindowService>();
        }

        private void OnDisable()
        {
            if (_team == TeamType.Player)
            {
                _inputService.OnChangeBlockingState -= Blocking;
            }
            else
            {
                _blockingLogic.OnStartBlocking -= Blocking;
            }
        } 

        private void Blocking(bool state)
        {
            IsBlocking = state;
            _animator.PlayBlocking(IsBlocking);
        }
        
        public void TakeDamage(int damage)
        {
            CurrentHealth -= IsBlocking ?  DamageAfterBlocking(damage) : damage;
            OnChangeHealth?.Invoke(_team);
            if (CurrentHealth <= 0)
            {
                OnDie?.Invoke(_team);
                _windowService.Open(WindowId.LoseWindow);
            }
        }

        public void AttachData(FighterData data, TeamType team)
        {
            MaxHealth = data.Health;
            CurrentHealth = MaxHealth;
            _team = team;
            
            if (_team == TeamType.Player)
            {
                _inputService.OnChangeBlockingState += Blocking;
            }
            else
            {
                _blockingLogic = GetComponent<BlockingLogic>();
                _blockingLogic.AttachData(data);
                _blockingLogic.OnStartBlocking += Blocking;

                GetComponent<AttackingLogic>().AttachData(data);
            }
        }

        public void RestoreHP(float healingPercentAfterStandUp)
        {
            CurrentHealth = (int)(MaxHealth * healingPercentAfterStandUp);
            OnChangeHealth?.Invoke(_team);
        }

        private int DamageAfterBlocking(int damage) 
            => damage - (int)Math.Round(damage * BlockingDamageReduction);
    }
}