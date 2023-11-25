using Items;
using UnityEngine;

namespace Components.Player
{
    public class MoveAroundComponent : MonoBehaviour
    {
        [SerializeField] private Transform spriteTransform;

        private float angularSpeed;
        
        private Vector3 _moveVector;
        private bool _isMoving;

        private PlayerItem _playerItem;

        public void Construct(PlayerItem playerItem)
        {
            _playerItem = playerItem;
            angularSpeed = playerItem.Speed;
            _moveVector = Vector3.back;
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
            angularSpeed = _playerItem.Speed;
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