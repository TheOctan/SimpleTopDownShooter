using OctanGames.Infrastructure.Factory;
using UnityEngine;

namespace OctanGames.Weapon
{
    public class Gun : MonoBehaviour, IWeapon
    {
        [SerializeField] private Transform[] _bulletPivots;

        private IGameFactory _gameFactory;
        public WeaponType WeaponType { get; private set; }

        public Gun Construct(IGameFactory gameFactory, WeaponType weaponType)
        {
            _gameFactory = gameFactory;
            WeaponType = weaponType;

            return this;
        }

        public void Fire()
        {
            SpawnBullets();
        }

        private void SpawnBullets()
        {
            foreach (Transform bulletPivot in _bulletPivots)
            {
                _gameFactory.CreateBullet(WeaponType, bulletPivot);
            }
        }

        public void Equip()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}