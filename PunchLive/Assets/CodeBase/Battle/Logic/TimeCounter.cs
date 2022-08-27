using System;
using System.Collections;
using CodeBase.Battle.AI;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Battle.Logic
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(Attacker))]
    public class TimeCounter : MonoBehaviour
    {
        private int _currentSecondsCount;
        private Coroutine _countUpdateCoroutine;
        private IWindowService _windowService;
        private IInputService _inputService;
        private Damageable _damageable;
        private AttackingLogic _attackingLogic;

        public event Action<int, int> Tick;

        private void Awake()
        {
            _windowService = ServiceLocator.Container.Single<IWindowService>();
            _inputService = ServiceLocator.Container.Single<IInputService>();
            _damageable = GetComponent<Damageable>();
            _attackingLogic = GetComponent<AttackingLogic>();
            _damageable.OnDie += StartCounting;
        }

        private void OnDisable()
        {
            _damageable.OnDie -= StartCounting;
        }

        public void StopCounting()
        {
            _attackingLogic.IsAttackEnable = true;
            _inputService.EnableInput();
            _windowService.Hud.UpdateCounterView(0);
            StopCoroutine(_countUpdateCoroutine);
            _countUpdateCoroutine = null;
        }

        private void StartCounting(TeamType team)
        {
            _attackingLogic.IsAttackEnable = false;
            _inputService.DisableInput();
            if (_countUpdateCoroutine == null)
            {
                _countUpdateCoroutine = StartCoroutine(CountSeconds(10));
            }
        }

        private IEnumerator CountSeconds(int initialSecondsCount)
        {
            _currentSecondsCount = initialSecondsCount + 1;
            while (_currentSecondsCount >= 0)
            {
                yield return new WaitForSeconds(1);
                _currentSecondsCount--;
                _windowService.Hud.UpdateCounterView(_currentSecondsCount);
                Tick?.Invoke(initialSecondsCount, _currentSecondsCount);
            }
        }
    }
}