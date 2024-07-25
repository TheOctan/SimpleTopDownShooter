using OctanGames.Weapon;

namespace OctanGames.Player
{
    public interface IWeaponSlot
    {
        void SwitchWeapon(WeaponType weaponType);
        void Fire();
    }
}