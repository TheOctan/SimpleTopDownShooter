using System.Collections.Generic;
using OctanGames.Infrastructure.Factory;
using OctanGames.Weapon;
using UnityEngine;

namespace OctanGames.Player
{
    public class WeaponSlot : MonoBehaviour, IWeaponSlot
    {
        private IGameFactory _gameFactory;

        private Dictionary<WeaponType, GameObject> _weapons;
        private GameObject _currentWeapon;

        public WeaponSlot Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _weapons = new Dictionary<WeaponType, GameObject>()
            {
                [WeaponType.Gun] = _gameFactory.CreateWeapon(WeaponType.Gun, transform, false),
                [WeaponType.ShotGun] = _gameFactory.CreateWeapon(WeaponType.ShotGun, transform, false),
            };

            SwitchWeapon(WeaponType.Gun);

            return this;
        }

        public void SwitchWeapon(WeaponType weaponType)
        {
            if (!_weapons.TryGetValue(weaponType, out GameObject weapon)) return;

            if (_currentWeapon != null)
            {
                _currentWeapon.SetActive(false);
            }
            _currentWeapon = weapon;
            _currentWeapon.SetActive(true);
        }

        public void Fire()
        {
            
        }
    }
}