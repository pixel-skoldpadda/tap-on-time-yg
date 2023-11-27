using System;
using System.Collections;
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
        private bool _isMoving360;

        private PlayerItem _playerItem;

        private Action _onMove360End;

        private float _moveAngle;
        
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
            if (!_isMoving)
            {
                return;
            }
            
            if (_isMoving360)
            {
                Move360();
            }
            else
            {
                Move();
            }
        }

        private void Move()
        {
            transform.RotateAround(Vector3.zero, _moveVector, angularSpeed * Time.deltaTime);
        }

        private void Move360()
        {
            if (_moveAngle >= 360f)
            {
                _isMoving360 = false;
                _isMoving = false;
                _moveAngle = 0f;
                _onMove360End?.Invoke();
            }
            else
            {
                float angle = _playerItem.ClaimRewardSpeed * Time.deltaTime;
                _moveAngle += angle;
                transform.RotateAround(Vector3.zero, _moveVector, angle);   
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

        public void StartMove360()
        {
            StartCoroutine(WaitMove360Routine());
        }

        private IEnumerator WaitMove360Routine()
        {
            yield return new WaitForSeconds(1f);
            
            _isMoving360 = true;
            _isMoving = true;
        }

        public void ResetComponent()
        {
            _isMoving = false;
            _isMoving360 = false;
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

        public Action OnMove360End
        {
            get => _onMove360End;
            set => _onMove360End = value;
        }
    }
}