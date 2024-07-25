using System;
using System.Collections;
using System.Linq;
using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private const float AIM_DELAY = 1f;

        [SerializeField] private float _aimRadius = 5f;
        [SerializeField] private float _turnSpeed = 12f;
        [SerializeField] private LayerMask _mask;

        private IInputService _inputService;
        private Vector3 _targetDirection;

        private readonly Collider2D[] _aimTargets = new Collider2D[5];
        private Transform _currentAimTarget;
        private int _lastHitCount;

        public PlayerAim Construct(IInputService inputService, float aimRadius)
        {
            _inputService = inputService;
            _aimRadius = aimRadius;

            return this;
        }

        private void OnEnable()
        {
            StartCoroutine(FindNearestTarget());
        }

        public void FixedUpdate()
        {
            _targetDirection = _currentAimTarget != null
                ? _currentAimTarget.position - transform.position
                : _inputService.Axis;

            PlayerRotate(_targetDirection);
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
                        .Where(e => e != null)
                        .OrderBy(e => (transform.position - e.transform.position).sqrMagnitude)
                        .FirstOrDefault()
                        ?.transform;
                }
                else
                {
                    _currentAimTarget = null;
                }

                _lastHitCount = hitCount;

                yield return new WaitForSeconds(AIM_DELAY);
            }

            _currentAimTarget = null;
        }

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