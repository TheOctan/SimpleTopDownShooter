namespace OctanGames.Weapon
{
    public interface IWeapon
    {
        WeaponType WeaponType { get; }
        void Fire();
        void Equip();
        void Hide();
    }
}