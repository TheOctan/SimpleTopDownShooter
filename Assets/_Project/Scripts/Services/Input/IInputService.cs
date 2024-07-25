using UnityEngine;

namespace OctanGames.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
        bool IsEquipGunButtonUp();
        bool IsEquipShotgunButtonUp();
    }
}