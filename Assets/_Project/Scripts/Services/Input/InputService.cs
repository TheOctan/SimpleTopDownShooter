using UnityEngine;

namespace OctanGames.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string HORIZONTAL = "Horizontal";
        protected const string VERTICAL = "Vertical";
        private const string FIRE_BUTTON = "Fire";
        private const string EQUIP_GUN_BUTTON = "EquipGun";
        private const string EQUIP_SHOTGUN_BUTTON = "EquipShotgun";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp() => SimpleInput.GetButtonUp(FIRE_BUTTON);
        public bool IsEquipGunButtonUp() => SimpleInput.GetButtonUp(EQUIP_GUN_BUTTON);
        public bool IsEquipShotgunButtonUp() => SimpleInput.GetButtonUp(EQUIP_SHOTGUN_BUTTON);

        protected static Vector2 SimpleInputAxis() =>
            new(SimpleInput.GetAxis(HORIZONTAL), SimpleInput.GetAxis(VERTICAL));
    }
}