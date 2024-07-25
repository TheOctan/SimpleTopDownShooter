using UnityEngine;

namespace OctanGames.Props
{
    public interface IDamagable
    {
        bool IgnoreAim { get; }
        void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection);
        void TakeDamage(float damage);
    }
}