using System;
using System.Collections;
using System.Linq;
using OctanGames.Props;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private const float AIM_DELAY = 1f;

        [Header("Properties")]
        [SerializeField] private float _aimRadius = 5f;
        [SerializeField] private float _turnSpeed = 12f;
        [SerializeField] private LayerMask _mask;

        [Header("Components")]
        [SerializeField] private GameObject _crossHairsPrefab;

        private IInputService _inputService;
        private Vector3 _targetDirection;

        private readonly Collider2D[] _aimTargets = new Collider2D[5];
        private Transform _currentAimTarget;
        private GameObject _crossHairs;

        public PlayerAim Construct(IInputService inputService, float aimRadius)
        {
            _inputService = inputService;
            _aimRadius = aimRadius;

            return this;
        }

        private void Start()
        {
            _crossHairs = Instantiate(_crossHairsPrefab);
            _crossHairs.SetActive(false);
        }

        private void OnEnable()
        {
            StartCoroutine(FindNearestTarget());
        }

        public void FixedUpdate()
        {
            if (_currentAimTarget != null)
            {
                _targetDirection = _currentAimTarget.position - transform.position;
                ShowCrossHairs();
            }
            else
            {
                _targetDirection = _inputService.Axis;
                HideCrossHairs();
            }

            PlayerRotate(_targetDirection);
        }

        private void ShowCrossHairs()
        {
            _crossHairs.SetActive(true);
            _crossHairs.transform.position = _currentAimTarget.position;
        }

        private void HideCrossHairs()
        {
            _crossHairs.SetActive(false);
        }

        private IEnumerator FindNearestTarget()
        {
            while (enabled)
            {
                Array.Clear(_aimTargets, 0, _aimTargets.Length);
                int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _aimRadius, _aimTargets, _mask);

                if (hitCount > 0)
                {
                    _currentAimTarget = _aimTargets
                        .Where(IsAimTarget)
                        .OrderBy(e => (transform.position - e.transform.position).sqrMagnitude)
                        .FirstOrDefault()
                        ?.transform;
                }
                else
                {
                    _currentAimTarget = null;
                }

                yield return new WaitForSeconds(AIM_DELAY);
            }

            _currentAimTarget = null;
        }

        private static bool IsAimTarget(Collider2D e) =>
            e != null
            && e.TryGetComponent(out IDamagable damageable)
            && !damageable.IgnoreAim;

        private void PlayerRotate(Vector3 direction)
        {
            if (direction == Vector3.zero) return;

            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
            Quaternion targetRotation = Quaternion.Euler(0, 0, lookRotation.eulerAngles.z);
            float turnStep = _turnSpeed * Time.fixedDeltaTime;

            transform.rotation = AccelerateRotation(transform.rotation, targetRotation, turnStep);
        }

        private static Quaternion AccelerateRotation(Quaternion rotation, Quaternion targetRotation, float turnSpeed) =>
            turnSpeed > 0
                ? Quaternion.Slerp(rotation, targetRotation, turnSpeed)
                : targetRotation;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireSphere(Vector3.zero, _aimRadius);
        }
    }
}