using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Player;
using OctanGames.Services;
using OctanGames.Services.Input;
using OctanGames.StaticData;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets,
            IInputService inputService,
            IStaticDataService staticData)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
        }

        public GameObject CreateHero(Vector3 initialPoint)
        {
            PlayerStaticData playerStaticData = _staticData.ForPlayer();

            GameObject player = _assets.Instantiate(AssetPath.PLAYER_PATH, initialPoint);
            player.GetComponent<PlayerMove>()
                .Construct(_inputService, playerStaticData.MovementSpeed);

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