using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private float _attackRadius = 5f;
        [Header("Components")]
        [SerializeField] private WeaponSlot _weaponSlot;

        private IInputService _inputService;

        public PlayerAttack Construct(IInputService inputService, float attackRadius)
        {
            _inputService = inputService;
            _attackRadius = attackRadius;

            return this;
        }

        private void Update()
        {
            if (!_inputService.IsAttackButtonUp()) return;

            Attack();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color  = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
        }

        private void Attack()
        {
            _weaponSlot.Fire();
        }
    }
}