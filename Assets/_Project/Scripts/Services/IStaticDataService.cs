using OctanGames.StaticData;
using OctanGames.Weapon;

namespace OctanGames.Services
{
    public interface IStaticDataService : IService
    {
        void LoadAllStaticData();
        PlayerStaticData ForPlayer();
        WeaponStaticData ForWeapon(WeaponType weaponType);
    }
}