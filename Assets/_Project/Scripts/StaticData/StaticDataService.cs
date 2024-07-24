using System.Collections.Generic;
using System.Linq;
using OctanGames.Data;
using OctanGames.Services;
using OctanGames.Weapon;
using UnityEngine;

namespace OctanGames.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string STATIC_DATA_PLAYER = "StaticData/Player";

        private PlayerStaticData _player;
        private Dictionary<WeaponType, WeaponStaticData> _weapons;

        public void LoadAllStaticData()
        {
            _player = Resources.Load<PlayerStaticData>(STATIC_DATA_PLAYER);

            _weapons = Resources
                .LoadAll<WeaponStaticData>("StaticData/Weapon")
                .ToDictionary(e => e.WeaponType, e => e);
        }

        public PlayerStaticData ForPlayer() => _player;
        public WeaponStaticData ForWeapon(WeaponType weaponType) => _weapons.GetValueOrNull(weaponType);
    }
}