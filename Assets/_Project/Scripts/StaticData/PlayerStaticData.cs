using UnityEngine;

namespace OctanGames.StaticData
{
    [CreateAssetMenu(fileName = "Player", menuName = "StaticData/Player", order = 0)]
    public class PlayerStaticData : ScriptableObject
    {
        public float MovementSpeed = 7;
        public float AttackRadius = 10f;
    }
}