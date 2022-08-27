using System.Collections;
using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Battle.Logic.Battle
{
    public class AttackReadingService : IAttackReadingService
    {
        private const float ReadingPauseValue = 0.3f;
        
        private readonly IInputService _inputService;
        private readonly IFighterFactory _fighterFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _readingPauseCoroutine;
        private string _buttonSequenceCode = string.Empty;
        private Dictionary<string, Attack> _attackDatabase;

        public AttackReadingService(IInputService inputService, IFighterFactory fighterFactory, ICoroutineRunner coroutineRunner)
        {
            _inputService = inputService;
            _fighterFactory = fighterFactory;
            _coroutineRunner = coroutineRunner;
        }

        public void InitAttacksDictionary()
        {
            _attackDatabase = new Dictionary<string, Attack>();
            foreach (Attack attack in _fighterFactory.Data.Attacks)
            {
                _attackDatabase.Add(attack.Binding.ButtonSequenceCode, attack);
            }
        }

        public void StartReading()
        {
            _inputService.OnButtonClick += ReadInput;
        }

        public void StopReading()
        {
            _inputService.OnButtonClick -= ReadInput;
        }

        private void ReadInput(ButtonType clickedButton)
        {
            if (_buttonSequenceCode.Length == 0)
            {
                _readingPauseCoroutine = _coroutineRunner.StartCoroutine(ReadingPause());
            }

            _buttonSequenceCode = string.Concat(_buttonSequenceCode, ((int) clickedButton).ToString());
        }

        private IEnumerator ReadingPause()
        {
            yield return new WaitForSeconds(ReadingPauseValue);
            Debug.Log("Generate Attack With Sequence " + _buttonSequenceCode);
            GenerateAttack(_buttonSequenceCode);
            _coroutineRunner.StopCoroutine(_readingPauseCoroutine);
            _buttonSequenceCode = string.Empty;
        }

        private void GenerateAttack(string buttonSequenceCode)
        {
            if (_attackDatabase.ContainsKey(buttonSequenceCode))
            {
                _fighterFactory.Attacker.Attacking(_attackDatabase[buttonSequenceCode]);
            }
            else if (buttonSequenceCode[0].ToString() != "3")
            {
                Attack attack = _attackDatabase[buttonSequenceCode[0].ToString()];
                _fighterFactory.Attacker.Attacking(attack);
                Debug.Log("Attack generated with code " + buttonSequenceCode[0]);
            }
        }
    }
}