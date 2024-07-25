using UnityEngine;

namespace OctanGames.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 10f;
        [SerializeField] private float _lifeTime = 3f;

        public Bullet Construct(float movementSpeed, float lifeTime)
        {
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

            transform.Translate(Vector3.up * movementDistance);
        }
    }
}