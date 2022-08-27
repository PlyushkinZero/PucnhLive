using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Battle.AI
{
    public class BlockingLogic : MonoBehaviour
    {
        private const float minimalPauseBeforeBlock = 0.1f;
        private const float maximalPauseBeforeBlock = 0.3f;       
        private const float minimalPauseAfterBlock = 1.0f;
        private const float maximalPauseAfterBlock = 3.0f;
        
        private IPlayerFactory _playerFactory;
        private float _chanceToBlock;
        
        public event Action<bool> OnStartBlocking;

        private void Awake()
        {
            _playerFactory = ServiceLocator.Container.Single<IPlayerFactory>();
            _playerFactory.Attacker.OnAttackInited += TryToBlock;
        }

        private void OnDisable()
        {
            _playerFactory.Attacker.OnAttackInited -= TryToBlock;
        }

        public void AttachData(FighterData data) 
            => _chanceToBlock = data.ChanceToBlock;

        private void TryToBlock()
        {
            if (RandomByChance.Check(_chanceToBlock))
            {
                StartCoroutine(ChangeBlockStateAfterPause(true, minimalPauseBeforeBlock, maximalPauseBeforeBlock));
                StartCoroutine(ChangeBlockStateAfterPause(false, minimalPauseAfterBlock, maximalPauseAfterBlock));
            }
        }

        private IEnumerator ChangeBlockStateAfterPause(bool state, float min, float max)
        {
            yield return new WaitForSeconds(Random.Range(min, max));
            OnStartBlocking?.Invoke(state);
        }
    }
}