using UnityEngine;

namespace OctanGames.Player
{
    public class CrossHairs : MonoBehaviour
    {
        [Header("Rotation")]
        [SerializeField] private float _rotateSpeed = 40;
        [SerializeField] private bool _clockwise;

        private void Update()
        {
            float rotateSpeed = _clockwise ? _rotateSpeed : -_rotateSpeed;
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }
}