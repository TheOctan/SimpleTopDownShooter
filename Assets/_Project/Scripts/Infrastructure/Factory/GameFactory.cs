using OctanGames.Infrastructure.AssetManagement;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(Vector3 initialPoint)
        {
            return _assets.Instantiate(AssetPath.PLAYER_PATH, initialPoint);
        }

        public GameObject CreateHud()
        {
            return _assets.Instantiate(AssetPath.HUD_PATH);
        }

        public void Cleanup()
        {
        }
    }
}