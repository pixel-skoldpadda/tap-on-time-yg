using UnityEngine;

namespace Player
{
    public class MoveAroundComponent : MonoBehaviour
    {
        [SerializeField] private float angularSpeed = 100f;
        [SerializeField] private Transform spriteTransform;
        
        private Vector3 _moveVector;
        private bool _isMoving;

        private void Start()
        {
            _moveVector = Vector3.back;
            StartMove();
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.RotateAround(Vector3.zero, _moveVector, angularSpeed * Time.deltaTime);
            }
        }

        public void StartMove()
        {
            _isMoving = true;
        }

        public void StopMove()
        {
            _isMoving = false;
        }

        public void ResetComponent()
        {
            _moveVector = Vector3.back;
            spriteTransform.Rotate(0, 0, 180);
        }

        public void ChangeDirection()
        {
            if (_moveVector.Equals(Vector3.back))
            {
                _moveVector = Vector3.forward;
                spriteTransform.Rotate(0, 0, -180);
            }
            else if (_moveVector.Equals(Vector3.forward))
            {
                _moveVector = Vector3.back;
                spriteTransform.Rotate(0, 0, 180);
            }
        }
    }
}