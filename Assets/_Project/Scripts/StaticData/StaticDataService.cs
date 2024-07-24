using OctanGames.Services;
using UnityEngine;

namespace OctanGames.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string STATIC_DATA_PLAYER = "StaticData/Player";

        private PlayerStaticData _playerStaticData;

        public void LoadAllStaticData()
        {
            _playerStaticData = Resources.Load<PlayerStaticData>(STATIC_DATA_PLAYER);
        }

        public PlayerStaticData ForPlayer() => _playerStaticData;
    }
}