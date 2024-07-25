using System.Collections.Generic;
using OctanGames.Infrastructure.Factory;
using OctanGames.Weapon;
using UnityEngine;

namespace OctanGames.Player
{
    public class WeaponSlot : MonoBehaviour, IWeaponSlot
    {
        private IGameFactory _gameFactory;

        private Dictionary<WeaponType, IWeapon> _weapons;
        private IWeapon _currentWeapon;

        public WeaponSlot Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _weapons = new Dictionary<WeaponType, IWeapon>()
            {
                [WeaponType.Gun] = _gameFactory.CreateWeapon(WeaponType.Gun, transform, false),
                [WeaponType.ShotGun] = _gameFactory.CreateWeapon(WeaponType.ShotGun, transform, false),
            };

            SwitchWeapon(WeaponType.Gun);

            return this;
        }

        public void SwitchWeapon(WeaponType weaponType)
        {
            if (!_weapons.TryGetValue(weaponType, out IWeapon weapon)) return;

            _currentWeapon?.Hide();
            _currentWeapon = weapon;
            _currentWeapon.Equip();
        }

        public void Fire()
        {
            _currentWeapon.Fire();
        }
    }
}