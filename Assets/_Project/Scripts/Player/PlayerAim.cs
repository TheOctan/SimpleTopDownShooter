using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private float _aimRadius = 5f;
        [SerializeField] private float _turnSpeed = 12f;

        private IInputService _inputService;
        private Vector3 _targetDirection;

        public PlayerAim Construct(IInputService inputService, float aimRadius)
        {
            _inputService = inputService;
            _aimRadius = aimRadius;

            return this;
        }

        public void FixedUpdate()
        {
            _targetDirection = _inputService.Axis;

            PlayerRotate(_targetDirection);
        }

        private void PlayerRotate(Vector3 direction)
        {
            if (direction == Vector3.zero) return;

            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
            Quaternion targetRotation = Quaternion.Euler(0,0, lookRotation.eulerAngles.z);
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