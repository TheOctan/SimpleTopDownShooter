using OctanGames.Weapon;
using UnityEngine;

namespace OctanGames.StaticData
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "StaticData/Weapon", order = 0)]
    public class WeaponStaticData : ScriptableObject
    {
        public WeaponType WeaponType;
        public GameObject WeaponPrefab;

        [Header("Bullet")]
        public Bullet BulletPrefab;
        public float BulletSpeed = 10f;
        public float BulletLifeTime = 3f;
        public float Damage = 1f;
    }
}