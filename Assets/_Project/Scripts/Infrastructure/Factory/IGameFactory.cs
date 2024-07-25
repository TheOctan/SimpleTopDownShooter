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
        IWeapon CreateWeapon(WeaponType weaponType, Transform parent, bool isActive);
        Bullet CreateBullet(WeaponType weaponType, Transform bulletPivot);
    }
}