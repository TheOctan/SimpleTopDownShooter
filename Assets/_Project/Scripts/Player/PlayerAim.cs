using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerAim : MonoBehaviour
    {
        [SerializeField] private float _aimRadius = 5f;

        private IInputService _inputService;
        private Vector2 _targetDirection;

        public PlayerAim Construct(IInputService inputService, float aimRadius)
        {
            _inputService = inputService;
            _aimRadius = aimRadius;

            return this;
        }

        public void Update()
        {
            Vector2 inputAxis = _inputService.Axis;
            if (inputAxis != Vector2.zero)
            {
                _targetDirection = inputAxis;
            }

            transform.up = _targetDirection;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _aimRadius);
        }
    }
}