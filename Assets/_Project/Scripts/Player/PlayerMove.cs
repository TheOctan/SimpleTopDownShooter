using OctanGames.Services.Input;
using UnityEngine;

namespace OctanGames.Player
{
    public class PlayerMove : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private float _movementSpeed = 5f;
        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidbody;

        private IInputService _inputService;

        public PlayerMove Construct(IInputService inputService, float movementSpeed)
        {
            _inputService = inputService;
            _movementSpeed = movementSpeed;

            return this;
        }

        private void FixedUpdate()
        {
            Vector2 movementVector = Vector2.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.EPSILON)
            {
                movementVector = _inputService.Axis;
                movementVector.Normalize();
            }

            _rigidbody.velocity = _movementSpeed * movementVector;
        }
    }
}