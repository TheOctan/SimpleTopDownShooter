using System;
using UnityEngine;

namespace OctanGames.Props
{
    public class Box : MonoBehaviour, IDamagable
    {
        [Header("Properties")]
        [SerializeField] private bool _ignoreAim;
        [SerializeField] private float _maxHitPoints = 2f;
        [Space]
        [SerializeField] private GameObject _destroyFx;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _brokenBox;

        private float _currentHitPoints;
        public bool IgnoreAim => _ignoreAim;

        private void Start()
        {
            _currentHitPoints = _maxHitPoints;
        }

        public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
        {
            TakeDamage(damage);
        }

        public void TakeDamage(float damage)
        {
            if (_currentHitPoints == 0) return;

            _currentHitPoints -= damage;

            if(_currentHitPoints <= 0)
            {
                Destroy(gameObject);
                Instantiate(_destroyFx, transform.position, Quaternion.identity);
            }
            else if (_currentHitPoints < _maxHitPoints)
            {
                _renderer.sprite = _brokenBox;
            }
        }
    }
}