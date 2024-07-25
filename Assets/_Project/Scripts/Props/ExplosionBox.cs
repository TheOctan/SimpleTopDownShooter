using UnityEngine;

namespace OctanGames.Props
{
    public class ExplosionBox : MonoBehaviour, IDamagable
    {
        [SerializeField] private bool _ignoreAim;
        [SerializeField] private GameObject _explosionPrefab;

        private bool _isDamaged;

        public bool IgnoreAim => _ignoreAim;

        public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
        {
            TakeDamage(damage);
        }

        public void TakeDamage(float damage)
        {
            if (_isDamaged) return;
            _isDamaged = true;

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}