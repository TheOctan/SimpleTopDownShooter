using System;
using System.Collections;
using OctanGames.Props;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private const float AIM_DELAY = 0.2f;

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
                ClearAimTarget();

                int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, _aimRadius, _aimTargets, _mask);

                float nearestAimDistance = float.PositiveInfinity;
                for (var i = 0; i < hitCount; i++)
                {
                    Collider2D aimTarget = _aimTargets[i];
                    float aimSqrDistance = AimSqrDistance(aimTarget);
                    if (!IsAimTarget(aimTarget) || aimSqrDistance >= nearestAimDistance) continue;

                    nearestAimDistance = aimSqrDistance;
                    _currentAimTarget = aimTarget.transform;
                }

                yield return new WaitForSeconds(AIM_DELAY);
            }

            _currentAimTarget = null;
        }

        private void ClearAimTarget()
        {
            Array.Clear(_aimTargets, 0, _aimTargets.Length);
            _currentAimTarget = null;
        }

        private static bool IsAimTarget(Component e) =>
            e.TryGetComponent(out IDamagable damageable)
            && !damageable.IgnoreAim;

        private float AimSqrDistance(Component e) =>
            (transform.position - e.transform.position).sqrMagnitude;

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