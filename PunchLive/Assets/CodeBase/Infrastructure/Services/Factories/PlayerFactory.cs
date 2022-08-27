using CodeBase.Battle.Logic;
using CodeBase.Infrastructure.PersistantProgress;
using CodeBase.Infrastructure.Services.AssetManagment;
using CodeBase.Infrastructure.Services.Factories.Interfaces;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly string PlayerPrefabPath = "Fighters/Player";
        private readonly string PlayerSpawnPointTag = "PlayerSpawnPoint";
        
        private readonly IAssetProvider _assetProvider;
        private readonly IPersistantProgressService _progress;
        public FighterData Data { get; private set; }
        public Attacker Attacker { get; private set; }

        public Damageable Damageable { get; private set; }

        public PlayerFactory(IAssetProvider assetProvider, IPersistantProgressService progress)
        {
            _assetProvider = assetProvider;
            _progress = progress;
        }

        public GameObject Spawn()
        {
            GameObject player = InitPrefab();
            LoadData(player);
            return player;
        }

        private GameObject InitPrefab()
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(PlayerSpawnPointTag);
            GameObject player = _assetProvider.Instantiate(PlayerPrefabPath, spawnPoint.transform.position);
            player.transform.rotation = spawnPoint.transform.rotation;
            return player;
        }

        private void LoadData(GameObject player)
        {
            Data = _progress.PlayerProgress.PlayerStats;
            
            Attacker = player.GetComponent<Attacker>();
            Attacker.AttachData(Data, TeamType.Player);
            
            Damageable = player.GetComponent<Damageable>();
            Damageable.AttachData(Data, TeamType.Player);
        }
    }
}