using System.Collections;
using CodeBase.Battle.Logic;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Battle.AI
{
    public class AttackingLogic : MonoBehaviour
    {
        private Attacker _attacker;
        private float _minimalPauseBetweenAttacks = 0.5f;
        private float _maximalPauseBetweenAttacks = 3.5f;

        public bool IsAttackEnable { get; set; } = true;

        private void Awake()
        {
            _attacker = GetComponent<Attacker>();
        }
        

        public void AttachData(FighterData data)
        {
            _minimalPauseBetweenAttacks = data.MinimalPauseBetweenAttacks;
            _maximalPauseBetweenAttacks = data.MaximalPauseBetweenAttacks; 
            StartCoroutine(AttackUpdate());
        }

        private IEnumerator AttackUpdate()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_minimalPauseBetweenAttacks,
                    _maximalPauseBetweenAttacks));
                if (IsAttackEnable)
                {
                    Attack randomAttack = _attacker.EnabledAttacks[Random.Range(0, _attacker.EnabledAttacks.Count)];
                    _attacker.Attacking(randomAttack);
                }
            }
        }
    }
}