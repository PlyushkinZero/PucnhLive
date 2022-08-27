using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.States.Interfaces;
using CodeBase.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Battle.Logic
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(TimeCounter))]
    public class LoseCheck : MonoBehaviour
    {
        private const float HealingPercentAfterStandUp = 0.5f;
        private Damageable _damageable;
        private TimeCounter _timeCounter;
        private IEnemyFactory _enemyFactory;
        private IGameStateMachine _gameStateMachine;

        private int _secondToStandUp;
        private bool _standUp;

        private void Awake()
        {
            _enemyFactory = ServiceLocator.Container.Single<IEnemyFactory>();
            _gameStateMachine = ServiceLocator.Container.Single<IGameStateMachine>();
            _timeCounter = GetComponent<TimeCounter>();
            _damageable = GetComponent<Damageable>();
            GetComponent<TimeCounter>().Tick += UpdateSecondsIndex;
        }

        private void OnDisable()
        {
            GetComponent<TimeCounter>().Tick -= UpdateSecondsIndex;
        }

        private void UpdateSecondsIndex(int initialSecondsCount, int currentSecondsCount)
        {
            if (initialSecondsCount == currentSecondsCount)
            {
                _standUp = RandomByChance.Check(_enemyFactory.Data.ChanceToStandUp);
                if (_standUp)
                {
                    _secondToStandUp = Random.Range(1, initialSecondsCount);
                }
            }

            if (_standUp && currentSecondsCount == _secondToStandUp)
            {
                _damageable.RestoreHP(HealingPercentAfterStandUp);
                _timeCounter.StopCounting();
            }

            if (_standUp == false && currentSecondsCount == 0)
            {
                _gameStateMachine.Enter<BattleResultState, TeamType>(TeamType.Player);
            }
        }
    }
}