using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Player;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;

        public GameFactory(IAssetProvider assets, IInputService inputService)
        {
            _assets = assets;
            _inputService = inputService;
        }

        public GameObject CreateHero(Vector3 initialPoint)
        {
            GameObject player = _assets.Instantiate(AssetPath.PLAYER_PATH, initialPoint);
            player.GetComponent<PlayerMove>().Construct(_inputService);

            return player;
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