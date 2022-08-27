using CodeBase.Battle.Logic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.Services.Windows
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Image _playerHpFiller;
        [SerializeField] private Image _enemyHpFiller;
        [SerializeField] private TextMeshProUGUI _textMesh;
        public Damageable Player { get; set; }
        public Damageable Enemy { get; set; }

        public void StartReadingHp()
        {
            Player.OnChangeHealth += SetFillerValue;
            Enemy.OnChangeHealth += SetFillerValue;
        }

        private void OnDisable()
        {
            Player.OnChangeHealth -= SetFillerValue;
            Enemy.OnChangeHealth -= SetFillerValue;
        }

        public void UpdateCounterView(int currentSecondsCount)
        {
            if (currentSecondsCount <= 0)
            {
                _textMesh.text = string.Empty;
            }
            else
            {
                _textMesh.text = currentSecondsCount.ToString();
            }
        }

        private void SetFillerValue(TeamType team)
        {
            switch (team)
            {
                case TeamType.Player:
                    _playerHpFiller.fillAmount = Player.CurrentHealth / (float)Player.MaxHealth;
                    break;
                case TeamType.Enemy:
                    _enemyHpFiller.fillAmount = Enemy.CurrentHealth / (float)Enemy.MaxHealth;
                    break;
            }
        }
    }
}