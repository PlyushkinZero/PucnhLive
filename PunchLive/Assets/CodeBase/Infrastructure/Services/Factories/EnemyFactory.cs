using CodeBase.Battle.Logic;
using CodeBase.Infrastructure.LevelManagement;
using CodeBase.Infrastructure.Services.AssetManagment;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly LevelManager _levelManager;
        private readonly string EnemyPrefabPath = "Fighters/Enemy";
        private readonly string EnemySpawnPointTag = "EnemySpawnPoint";
        public FighterData Data { get; private set; }
        public Attacker Attacker { get; private set; }
        public Damageable Damageable { get; private set; }

        public EnemyFactory(IAssetProvider assetProvider, LevelManager levelManager)
        {
            _assetProvider = assetProvider;
            _levelManager = levelManager;
        }

        public GameObject Spawn()
        {
            GameObject enemy = InitPrefab();
            LoadData(enemy);
            return enemy;
        }

        private GameObject InitPrefab()
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(EnemySpawnPointTag);
            GameObject enemy = _assetProvider.Instantiate(EnemyPrefabPath, spawnPoint.transform.position);
            enemy.transform.rotation = spawnPoint.transform.rotation;
            return enemy;
        }

        private void LoadData(GameObject enemy)
        {
            Data = _levelManager.CurrentLevel.Enemy;
            
            Attacker = enemy.GetComponent<Attacker>();
            Attacker.AttachData(Data, TeamType.Enemy);
            
            Damageable = enemy.GetComponent<Damageable>();
            Damageable.AttachData(Data, TeamType.Enemy);
        }
    }
}