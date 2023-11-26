using Items;
using UnityEngine;

namespace Components.Player
{
    public class MoveAroundComponent : MonoBehaviour
    {
        private Vector3 _defaultScale = new(.5f, 0.5f, 0);
        private Vector3 _reverseScale = new(-.5f, 0.5f, 0);
        
        [SerializeField] private Transform spriteTransform;

        private float angularSpeed;
        
        [SerializeField] private Vector3 _moveVector;
        private bool _isMoving;

        private PlayerItem _playerItem;

        public void Construct(PlayerItem playerItem)
        {
            _playerItem = playerItem;
            angularSpeed = playerItem.Speed;
            _moveVector = Vector3.back;
        }

        private void Awake()
        {
            _defaultScale = spriteTransform.localScale;
            _reverseScale = new Vector3(-_defaultScale.x, _defaultScale.y, _defaultScale.z);
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
            spriteTransform.localScale = _defaultScale;
        }

        public void ChangeDirection()
        {
            if (_moveVector.Equals(Vector3.back))
            {
                _moveVector = Vector3.forward;
                spriteTransform.localScale = _reverseScale;
            }
            else if (_moveVector.Equals(Vector3.forward))
            {
                _moveVector = Vector3.back;
                spriteTransform.localScale = _defaultScale;
            }
        }

        public void ChangeSpeed(float speed)
        {
            angularSpeed = speed;
        }
    }
}