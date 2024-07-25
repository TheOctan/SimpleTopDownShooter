using OctanGames.Services.Input;
using OctanGames.Weapon;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private IInputService _inputService;
        private IWeaponSlot _weaponSlot;

        public PlayerAttack Construct(IInputService inputService,
            IWeaponSlot weaponSlot)
        {
            _inputService = inputService;
            _weaponSlot = weaponSlot;

            return this;
        }

        private void Update()
        {
            if (_inputService.IsAttackButtonUp())
            {
                Attack();
            }

            if (_inputService.IsEquipGunButtonUp())
            {
                _weaponSlot.SwitchWeapon(WeaponType.Gun);
            }
            else if(_inputService.IsEquipShotgunButtonUp())
            {
                _weaponSlot.SwitchWeapon(WeaponType.ShotGun);
            }
        }

        private void Attack()
        {
            _weaponSlot.Fire();
        }
    }
}