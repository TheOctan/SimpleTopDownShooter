using OctanGames.Props;
using UnityEngine;

namespace OctanGames.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private LayerMask _collisionMask;

        public Bullet Construct(float movementSpeed, float lifeTime, float damage)
        {
            _damage = damage;
            _movementSpeed = movementSpeed;
            _lifeTime = lifeTime;

            return this;
        }

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        private void Update()
        {
            float movementDistance = _movementSpeed * Time.deltaTime;
            CheckCollisions(movementDistance);
            transform.Translate(Vector3.up * movementDistance);
        }

        private void CheckCollisions(float moveDistance)
        {
            Transform t = transform;
            Vector3 position = t.position;
            Vector3 up = t.up;

            Debug.DrawLine(position, position + up, Color.red);

            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.up, moveDistance, _collisionMask);
            if (hit.collider == null) return;

            HitObject(hit.collider, hit.point);
        }

        private void HitObject(Collider2D hitCollider, Vector3 hitPoint)
        {
            if (hitCollider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeHit(_damage, hitPoint, transform.up);
            }
            Destroy(gameObject);
        }
    }
}