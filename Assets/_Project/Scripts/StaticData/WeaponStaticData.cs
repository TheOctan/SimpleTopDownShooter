using OctanGames.Weapon;
using UnityEngine;

namespace OctanGames.StaticData
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "StaticData/Weapon", order = 0)]
    public class WeaponStaticData : ScriptableObject
    {
        public WeaponType WeaponType;
        public GameObject WeaponPrefab;
    }
}