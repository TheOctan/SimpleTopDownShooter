using OctanGames.Services;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 initialPoint);
        GameObject CreateHud();
        void Cleanup();
    }
}