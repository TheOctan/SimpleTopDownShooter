using OctanGames.Infrastructure.AssetManagement;
using OctanGames.Player;
using OctanGames.Services;
using OctanGames.Services.Input;
using OctanGames.StaticData;
using OctanGames.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OctanGames.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assets,
            IInputService inputService,
            IStaticDataService staticData)
        {
            _assets = assets;
            _inputService = inputService;
            _staticData = staticData;
        }

        public GameObject CreateHero(Vector3 initialPoint)
        {
            PlayerStaticData playerStaticData = _staticData.ForPlayer();

            GameObject player = _assets.Instantiate(AssetPath.PLAYER_PATH, initialPoint);
            player.GetComponent<PlayerMove>()
                .Construct(_inputService, playerStaticData.MovementSpeed);

            WeaponSlot weaponSlot = player.GetComponentInChildren<WeaponSlot>()
                .Construct(this);

            player.GetComponent<PlayerAttack>()
                .Construct(_inputService, weaponSlot);

            player.GetComponent<PlayerAim>()
                .Construct(_inputService, playerStaticData.AimRadius);

            return player;
        }

        public GameObject CreateHud()
        {
            return _assets.Instantiate(AssetPath.HUD_PATH);
        }

        public IWeapon CreateWeapon(WeaponType weaponType, Transform parent, bool isActive)
        {
            WeaponStaticData weaponStaticData = _staticData.ForWeapon(weaponType);
            GameObject weapon = Object.Instantiate(weaponStaticData.WeaponPrefab, parent);
            weapon.SetActive(isActive);

            return weapon.GetComponent<Gun>().Construct(this, weaponType);
        }

        public Bullet CreateBullet(WeaponType weaponType, Transform bulletPivot)
        {
            WeaponStaticData weaponStaticData = _staticData.ForWeapon(weaponType);

            return Object
                .Instantiate(weaponStaticData.BulletPrefab, bulletPivot.position, bulletPivot.rotation)
                .Construct(weaponStaticData.BulletSpeed, weaponStaticData.BulletLifeTime);
        }

        public void Cleanup()
        {
        }
    }
}