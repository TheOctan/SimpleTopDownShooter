using OctanGames.Services;
using OctanGames.Weapon;
using UnityEngine;

namespace OctanGames.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(Vector3 initialPoint);
        GameObject CreateHud();
        void Cleanup();
        GameObject CreateWeapon(WeaponType weaponType, Transform parent, bool isActive);
    }
}