using System;
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
            throw new NotImplementedException();
        }

        public void Cleanup()
        {
        }
    }
}